using CSharpBackend.API.Models.DataTransferObjects;
using Azure.AI.OpenAI;
using CSharpBackend.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CSharpBackend.API.ResearcherClasses
{


    public class AIGameResearcher
    {
    
        
        private readonly string deploymentName;
        private readonly string chatRequestSystemMessage;

        private readonly string chatRequestUserMessage;

        private readonly IGamesRepository gamesRepository;


        public readonly string boardGameName;


        public AIGameResearcher(string boardGameName, IGamesRepository gamesRepository)
        {

            if (string.IsNullOrWhiteSpace(boardGameName))
            {
                throw new ArgumentNullException(nameof(boardGameName),"BoardGameName must not be null...");
            }

            this.boardGameName = boardGameName;
            this.gamesRepository = gamesRepository;


            string JSONSettingsContent = File.ReadAllText(@"ResearcherClasses\aisearchsettings.json");
            AISearchSettings aiSearchSettings = JsonSerializer.Deserialize<AISearchSettings>(JSONSettingsContent);             

        
            deploymentName = aiSearchSettings.deploymentName;
            chatRequestSystemMessage = aiSearchSettings.chatRequestSystemMessage;
            chatRequestUserMessage = aiSearchSettings.chatRequestUserMessage;


        }


    public async Task<BoardGameDto?> GetBoardGameResearchObj()
    {
            var boardGameResearchObject = await ResearchGame();
            return boardGameResearchObject;
    }

    private async Task<BoardGameDto?> ResearchGame()
    {
        try 
        {
            var boardGameResearchObject = await TryToResearchGame();
            return boardGameResearchObject;
        }
        catch (ArgumentNullException argumentNullException)
        {
            Console.WriteLine(argumentNullException);
            Console.WriteLine("DeploymentName, API key and Completions Options must be initialised with a correct value and may not be null...");
            throw;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine("There was an error executing this code. Potential causes: [Lack of Internet Connection, Exhaustion of API credit...]");
            throw;
        }
        }
    



    private async Task<BoardGameDto?> TryToResearchGame()
    {

        var isNewToDatabase = await CheckIsNewGameToDB(boardGameName);
        if (!isNewToDatabase)
        {
            return new NullBoardGameDto();
        }

        var openAIKey = Environment.GetEnvironmentVariable("OPEN_AI_KEY",EnvironmentVariableTarget.User);
       
        if (string.IsNullOrEmpty(openAIKey))
        {
            throw new ArgumentNullException("Open AI API key is empty or undefined...");
        }


        var client = new OpenAIClient(openAIKey, new OpenAIClientOptions());

        if (
            string.IsNullOrEmpty(deploymentName)
        ||  string.IsNullOrEmpty(chatRequestSystemMessage) 
        ||  string.IsNullOrEmpty(chatRequestUserMessage)
        )
        {
            throw new ArgumentNullException("chatRequestSystemMessage and chatRequestUserMessage must both be defined...");
        }


        var chatCompletionOptions = new ChatCompletionsOptions()
        {

            DeploymentName = deploymentName,
            Messages = { 
                new ChatRequestSystemMessage(chatRequestSystemMessage),
                new ChatRequestUserMessage(chatRequestUserMessage + boardGameName)            
                },

        };

        //Connect to OpenAI API and make request
        var openAIPromptResponse = "";

        await foreach (StreamingChatCompletionsUpdate chatUpdate in client.GetChatCompletionsStreaming(chatCompletionOptions))
        {

           openAIPromptResponse += chatUpdate.ContentUpdate; 

        }

        Console.Write(openAIPromptResponse);
        // var boardGameResearchDto = CreateBoardGameResearchDto(openAIPromptResponse);


        // var isRealBoardGame = boardGameResearchDto["boardGameExists"];
        // if (!isRealBoardGame)
        // {
        //     return new NullBoardGameDto();
        // }

        // var boardGameDto = CreateBoardGameDto(boardGameResearchDto);
        
        var boardGameDto = new RealBoardGameDto();
        return boardGameDto;
        
        
    }


    private async Task<bool> CheckIsNewGameToDB(string BoardGameName)
    {
        try
        {
            var boardGameEntry = await gamesRepository.GetByNameAsync(BoardGameName);
            if (boardGameEntry.BoardGameName == null)
            {
                return true;
            }
            return false;
        }
        catch (System.NullReferenceException)
        {
            Console.WriteLine("It is not currently possible to interact with Games Repository...");
            throw;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }

    
    }


    private RealBoardGameDto CreateBoardGameDto(BoardGameResearchDto boardGameResearchDto)
    {
        
        var realBoardGameDto = new RealBoardGameDto()
        {
            boardGameId = Guid.NewGuid(),
            boardGameDescription = boardGameResearchDto.boardGameDescription,
            boardGamePrice = boardGameResearchDto.boardGamePrice,
            boardGameBuyUrl = boardGameResearchDto.boardGameBuyUrl,
            boardGameGenre = boardGameResearchDto.boardGameGenre
        };

        return realBoardGameDto;
    }
    


    }











}

