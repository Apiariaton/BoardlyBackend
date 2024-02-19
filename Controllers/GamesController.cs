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

        private readonly CSharpBackendDbContext dbContext;

        public GamesController(CSharpBackendDbContext dbContext, IGamesRepository gamesRepository)
        {
            this.dbContext = dbContext;
            this.gamesRepository = gamesRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(
        [FromQuery]   string? columnToFilter, 
        [FromQuery]   string? rowToMatch,
        [FromQuery]   string? columnToSort
        [FromQuery]   bool? isAscending,
        [FromQuery]   int startPageNumber = 1,
        [FromQuery]   int resultsPerPage = 100,
        )
        {

            var boardGamesList = await gamesRepository.GetAllAsync(
                columnToFilter, 
                rowToMatch,
                columnToSort
                isAscending,
                startPageNumber,
                resultsPerPage,
            );

            var boardGamesDtoList = new List<BoardGameDto>();

            foreach (boardGame in boardGamesList)
            {
                boardGamesDto.Add(
                    new BoardGameDto()
                    {
                        boardGameId = boardGame.boardGameId,
                        boardGameName = boardGame.boardGameName,
                        boardGameDescription = boardGame.boardGameDescription,
                        boardGamePrice = boardGame.boardGamePrice,
                        boardGameBuyUrl = boardGame.boardGameBuyUrl,
                        boardGameGenre = boardGame.boardGameGenre
                    }
                );
            };

            return Ok(boardGamesDtoList);

        }



        [HttpPost]

        public async Create([FromBody] string boardGameName)
        {


            
            
            var boardGameCreatedFromPostRequest = await gamesRepository.CreateAsync();
            




        }













    }









}