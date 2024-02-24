using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CSharpBackend.API.Data;
using CSharpBackend.API.Models.Domain;
using CSharpBackend.API.Models.DataTransferObjects;
using CSharpBackend.API.Repositories;


namespace CSharpBackend.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class GamesController : ControllerBase
    {

        private readonly CSharpBackendDBContext dbContext;
        private readonly IGamesRepository gamesRepository;

        public GamesController(CSharpBackendDBContext dbContext, IGamesRepository gamesRepository)
        {
            this.dbContext = dbContext;
            this.gamesRepository = gamesRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery]   string? columnToFilter, 
        [FromQuery]   string? rowToMatch,
        [FromQuery]   string? columnToSort,
        [FromQuery]   bool isAscending,
        [FromQuery]   int startPageNumber = 1,
        [FromQuery]   int resultsPerPage = 100
        )
        {

            var boardGamesList = await gamesRepository.GetAllAsync(
                columnToFilter, 
                rowToMatch,
                columnToSort,
                isAscending,
                startPageNumber,
                resultsPerPage
            );

            var boardGamesDtoList = new List<BoardGameDto>();

            foreach (BoardGame boardGame in boardGamesList)
            {
                boardGamesDtoList.Add(
                    new RealBoardGameDto()
                    {
                        boardGameId = boardGame.BoardGameId,
                        boardGameName = boardGame.BoardGameName,
                        boardGameDescription = boardGame.BoardGameDescription,
                        boardGamePrice = boardGame.BoardGamePrice,
                        boardGameBuyUrl = boardGame.BoardGameBuyUrl,
                        boardGameGenre = boardGame.BoardGameGenre
                    }
                );
            };

            return Ok(boardGamesDtoList);

        }



        [HttpPost]

        public async Task<IActionResult> Create([FromBody] RealBoardGameDto boardGameDto)
        {


        
            var boardGameCreatedFromPostRequest = await gamesRepository.CreateAsync(boardGameDto);
            
            if (boardGameCreatedFromPostRequest is RealBoardGameDto)
            {
                return Ok(boardGameCreatedFromPostRequest);
            }
            else
            {
                return BadRequest(boardGameCreatedFromPostRequest);
            }
        
        
        }













    }









}