using CSharpBackend.API.Models.DataTransferObjects;

namespace CSharpBackend.ResearcherClasses
{


    public class AIGameResearcher
    {
    
        
        private readonly string deploymentName = "gpt-3.5-turbo-0125";

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

            DeploymentName = deploymentName,
            Messages = { 
                new ChatSystemMessage(),
                new ChatRequestUserMessage            
                },

        };

        try 
        {
            //Connect to Open AI API and make a request


        }
        catch ()
        {
            throw new Exception("Error connecting to Open AI API");
        }
        catch ()
        {
            throw new Exception("Error occurred when attempting to use API key provided");
        }
        catch ()
        {
            throw new Exception("Error occurred to lack of Open AI API credits");
        }


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