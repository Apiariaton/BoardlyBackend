namespace CSharpBackend.ResearcherClasses
{


    public class AIGameResearcher
    {
    

    public class AIGameResearcher(string BoardGameName)
    {
        string boardGameName = BoardGameName;
    }


    public async Task<BoardGameDto?> GetBoardGameResearchObj()
    {
        try 
        {
            var boardGameResearchObject = TryToGetBoardGameResearchObj();
            return boardGameResearchObject;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }


    public async Task<BoardGameDto?> TryToGetBoardGameResearchObj()
    {
        var boardGameResearchObject = await ResearchGame();
        
        var isRealBoardGame = boardGameResearchObject["isRealBoardGame"];
        var isNewToDatabase = boardGameResearchObject["isNewToDatabase"];

        if (isRealBoardGame && isNewToDatabase)
        {
            return boardGameResearchObject;
        }
        return {};
    }



    public async Task<BoardGameDto?> ResearchGame()
    {
        





    }

    





















    }

}