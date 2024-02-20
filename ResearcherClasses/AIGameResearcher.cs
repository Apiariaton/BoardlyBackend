using CSharpBackend.API.Models.DataTransferObjects;

namespace CSharpBackend.ResearcherClasses
{


    public class AIGameResearcher
    {
    
    

        public AIGameResearcher(string BoardGameName)
        {

            if (string.IsNullOrWhiteSpace(BoardGameName))
            {
                throw new ArgumentNullException(nameof(BoardGameName),"BoardGameName must not be null...");
            }

            string boardGameName = BoardGameName;
        
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
        catch (Exception exception)
        {
            Console.WriteLine(exception);   
        }
        catch ()
        {

        }
        catch ()
        {


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
            throw new Exception("Open AI API key is empty or undefined...");
        }

        var client = new OpenAIClient(openAIKey, new OpenAIClientOptions());

        var chatCompletionOptions = new ChatCompletionsOptions()
        {

            DeploymentName = "",
            Messages = { new ChatRequestUserMessage()},
            Tools = {researchGameTool},

        };

        try 
        {
            //Connect to Open AI API and make a request


        }
        catch ()//Exception produced as a result of Open AI API Connection
        {
            throw new Exception("Error connecting to Open AI API");
        }
        catch ()//Exception produced as a result of lack of Open AI API credits
        {
            throw new Exception("Error occurred to lack of Open AI API credits");
        }
        catch ()//Exception produced as a result of lack of Open AI API credits
        {
            throw new Exception("Error occurred to lack of Open AI API credits");
        }


        var isRealBoardGame = gameResearchObject["boardGameExists"];
        if (!isRealBoardGame)
        {
            return new BoardGameDto();
        }

        var boardGameResearchObject = createBoardGameDto(gameResearchObject);
        
        
    }

    





















    }

}