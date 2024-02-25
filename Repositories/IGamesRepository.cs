using CSharpBackend.API.Models.DataTransferObjects;
using CSharpBackend.API.Models.Domain;

namespace CSharpBackend.API.Repositories
{



    public interface IGamesRepository
    {

        Task<List<BoardGame>> GetAllAsync(
            string? columnToFilter = null, 
            string? rowToMatch = null,
            string? columnToSort = null,
            bool isAscending = true,
            int startPageNumber = 1,
            int resultsPerPage = 100
        );

        Task<BoardGame?> GetByName(
            string BoardGameName
        );

        Task<BoardGame?> UpdateAsync(
            Guid boardGameId,
            BoardGame boardGame
        );

        Task<BoardGame?> DeleteAsync(
            Guid boardGameId
        );

        Task<BoardGame> CreateAsync(
            RealBoardGameDto boardGame
        );


    }



}