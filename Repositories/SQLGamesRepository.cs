using Microsoft.EntityFrameworkCore;
using CSharpBackend.API.Data;
using CSharpBackend.API.Models.Domain;

namespace CSharpBackend.API.Repositories
{


    public class SQLGamesRepository : IGamesRepository
    {


        private readonly CSharpBackendDBContext dbContext;

        public SQLGamesRepository(CSharpBackendDBContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<BoardGame> CreateAsync(BoardGame boardGame)
        {

            await dbContext.BoardGames.AddAsync(boardGame);
            await dbContext.SaveChangesAsync();
            return boardGame;

        }

        public async Task<List<BoardGame>> GetAllAsync(
            string? columnToFilter = null, 
            string? rowToMatch = null,
            string? columnToSort = null,
            bool isAscending = true,
            int startPageNumber = 1,
            int resultsPerPage = 100,
            )
        {

            var listOfBoardGames = dbContext.BoardGames.Include("boardGameName").Include("boardGameGenre").Include("boardGamePrice").AsQueryable();
            
            //Filter by columnToFilter and rowToMatch
            if (string.IsNullOrWhiteSpace(columnToFilter) == false && string.IsNullOrWhiteSpace(rowToMatch) == false)
            {
                if (columnToFilter.Equals("boardGameName"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x => x.boardGameName.Contains(rowToMatch));
                }
                else if (columnToFilter.Equals("boardGameGenre"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x=> x.boardGameGenre.Equals(rowToMatch));
                }
                else if (columnToFilter.Equals("boardGamePrice"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x=> x.boardGamePrice <= rowToMatch);
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("boardGameName", StringComparison.OrdinalIgnoreCase))
                {
                    listOfWalks = isAscending ? listOfWalks.OrderBy(x => x.boardGameName) : listOfWalks.OrderByDescending(x => x.boardGameName);

                }
                else if (sortBy.Equals("boardGamePrice"))
                {
                    listOfWalks = isAscending ? listOfWalks.OrderBy(x => x.boardGamePrice) : listOfWalks.OrderByDescending(x => x.boardGamePrice);
                }

            }

            //Pagination
            var numberOfBoardGamesToSkip = (startingPage - 1) * resultsPerPage;

            return await listOfBoardGames.Skip(numberOfBoardGamesToSkip).Take(resultsPerPage).ToListAsync();


        }


        public async Task<BoardGame?> UpdateAsync(Guid boardGameId, BoardGame boardGame)
        {

            var boardGameLocatedByID = await dbContext.BoardGames.FirstOrDefault(x => x.boardGameId == boardGameId);

            if (boardGameLocatedByID == null)
            {
                return {};
            }

            boardGameLocatedByID.boardGameName = boardGame.boardGameName;
            boardGameLocatedByID.boardGameDescription = boardGame.boardGameDescription;
            boardGameLocatedByID.boardGamePrice = boardGame.boardGamePrice;
            boardGameLocatedByID.boardGameBuyUrl = boardGame.boardGameBuyUrl;
            boardGameLocatedByID.boardGameGenre = boardGame.boardGameGenre;

            dbContext.SaveChangesAsync();
            return boardGameLocatedByID;

        }


        public async Task<BoardGame?> DeleteAsync(Guid boardGameId)
        {


            var boardGameLocatedByID = await dbContext.BoardGames.FirstOrDefault(x => x.boardGameId == boardGameId);

            if (boardGameLocatedByID == null)
            {
                return {};
            }

            dbContext.Walks.Remove(boardGameLocatedByID);
            await dbContext.SaveChangesAsync();
            return boardGameLocatedByID;


        }




    }





}