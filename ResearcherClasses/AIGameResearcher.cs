using CSharpBackend.API.Models.DataTransferObjects;
using Azure.AI.OpenAI;

namespace CSharpBackend.ResearcherClasses
{


    public class AIGameResearcher
    {
    
        
        private readonly string deploymentName = "gpt-3.5-turbo-0125";
        private readonly string chatRequestSystemMessage = "";

        private readonly string chatRequestUserMessage = "";


        public AIGameResearcher(string BoardGameName)
        {

            if (string.IsNullOrWhiteSpace(BoardGameName))
            {
                throw new ArgumentNullException(nameof(BoardGameName),"BoardGameName must not be null...");
            }

            string boardGameName = BoardGameName;
            string deploymentName = this.deploymentName;
            string chatRequestSystemMessage = this.chatRequestSystemMessage;
            string chatRequestUserMessage = this.chatRequestUserMessage;
        
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
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            Console.WriteLine("There was an error executing this code. Potential causes: [Lack of Internet Connection, Exhaustion of API Tokens...]");
        }
        }
    



    private async Task<BoardGameDto?> TryToResearchGame()
    {

        var isNewToDatabase = await checkIsNewGameToDB(boardGameName);
        if (!isNewToDatabase)
        {
            return new BoardGameDto();
        }

        var openAIKey = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
       
        if (string.IsNullOrEmpty(openAIKey))
        {
            throw new ArgumentNullException("Open AI API key is empty or undefined...");
        }

        var client = new OpenAIClient(openAIKey, new OpenAIClientOptions());

        var chatCompletionOptions = new ChatCompletionsOptions()
        {

            DeploymentName = deploymentName,
            Messages = { 
                new ChatRequestSystemMessage(chatRequestSystemMessage),
                new ChatRequestUserMessage(chatRequestUserMessage + boardGameName)            
                },

        };

        //Connect to OpenAI API and make request

      

        var isRealBoardGame = gameResearchObject["boardGameExists"];
        if (!isRealBoardGame)
        {
            return new BoardGameDto();
        }

        var boardGameResearchObject = createBoardGameDto(gameResearchObject);
        return boardGameResearchObject;
        
        
    }

    









    }











}

