using Microsoft.EntityFrameworkCore;
using CSharpBackend.API.Data;
using CSharpBackend.API.Models.Domain;
using CSharpBackend.API.Models.DataTransferObjects;

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
            int resultsPerPage = 100
            )
        {
        

            var listOfBoardGames = dbContext.BoardGames.AsQueryable();
            
            //Filter by columnToFilter and rowToMatch
            if (string.IsNullOrWhiteSpace(columnToFilter) == false && string.IsNullOrWhiteSpace(rowToMatch) == false)
            {
                if (columnToFilter.Equals("boardGameName"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x => x.BoardGameName.Contains(rowToMatch));
                }
                else if (columnToFilter.Equals("boardGameGenre"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x=> x.BoardGameGenre.Equals(rowToMatch));
                }
                else if (columnToFilter.Equals("boardGamePrice"))
                {
                    listOfBoardGames = listOfBoardGames.Where(x=> x.BoardGamePrice <= float.Parse(rowToMatch));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(columnToSort)==false)
            {
                if (columnToSort.Equals("boardGameName", StringComparison.OrdinalIgnoreCase))
                {
                    listOfBoardGames = isAscending ? listOfBoardGames.OrderBy(x => x.BoardGameName) : listOfBoardGames.OrderByDescending(x => x.BoardGameName);

                }
                else if (columnToSort.Equals("boardGamePrice"))
                {
                    listOfBoardGames = isAscending ? listOfBoardGames.OrderBy(x => x.BoardGamePrice) : listOfBoardGames.OrderByDescending(x => x.BoardGamePrice);
                }

            }

            //Pagination
            var numberOfBoardGamesToSkip = (startPageNumber - 1) * resultsPerPage;

            return await listOfBoardGames.Skip(numberOfBoardGamesToSkip).Take(resultsPerPage).ToListAsync();


        }


        public async Task<BoardGame?> GetByNameAsync(string boardGameName)
        {
            var boardGameLocatedByName = await dbContext.BoardGames.FirstOrDefaultAsync(x => x.BoardGameName == boardGameName);

            if (boardGameLocatedByName == null)
            {
                return null;

            }

            return boardGameLocatedByName; 

        }



        public async Task<BoardGame?> UpdateAsync(Guid boardGameId, BoardGame boardGame)
        {

            var boardGameLocatedByID = await dbContext.BoardGames.FirstOrDefaultAsync(x => x.BoardGameId == boardGameId);

            if (boardGameLocatedByID == null)
            {
                return null;
            }

            boardGameLocatedByID.BoardGameName = boardGame.BoardGameName;
            boardGameLocatedByID.BoardGameDescription = boardGame.BoardGameDescription;
            boardGameLocatedByID.BoardGamePrice = boardGame.BoardGamePrice;
            boardGameLocatedByID.BoardGameBuyUrl = boardGame.BoardGameBuyUrl;
            boardGameLocatedByID.BoardGameGenre = boardGame.BoardGameGenre;

            await dbContext.SaveChangesAsync();
            return boardGameLocatedByID;

        }


        public async Task<BoardGame?> DeleteAsync(Guid boardGameId)
        {


            var boardGameLocatedByID = await dbContext.BoardGames.FirstOrDefaultAsync(x => x.BoardGameId == boardGameId);

            if (boardGameLocatedByID == null)
            {
                return null;
            }

            dbContext.BoardGames.Remove(boardGameLocatedByID);
            await dbContext.SaveChangesAsync();
            
            
            return boardGameLocatedByID;


        }




    }





}