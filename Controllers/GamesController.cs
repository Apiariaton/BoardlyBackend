using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CSharpBackend.API.Data;
using CSharpBackend.API.Models.Domain;
using CSharpBackend.API.Models.DataTransferObjects;
using CSharpBackend.API.Repositories;
using CSharpBackend.API.ResearcherClasses;
using CSharpBackend.API.ModeratorClasses;


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
        public async Task<IActionResult> GetAllAsync(
        [FromQuery]   string? columnToFilter, 
        [FromQuery]   string? rowToMatch,
        [FromQuery]   string? columnToSort,
        [FromQuery]   bool isAscending = true,
        [FromQuery]   int startPageNumber = 1,
        [FromQuery]   int resultsPerPage = 100
        )
        {

            try
            {
            var boardGamesList = await gamesRepository.GetAllAsync(
                columnToFilter, 
                rowToMatch,
                columnToSort,
                isAscending,
                startPageNumber,
                resultsPerPage
            );

            var boardGamesDtoList = new List<RealBoardGameDto>();


            foreach (BoardGame boardGame in boardGamesList)
            {

                boardGamesDtoList.Add(
                    new RealBoardGameDto()
                    {
                        boardGameId = boardGame.BoardGameId,
                        boardGameName = boardGame.BoardGameName,
                        boardGameDescription = boardGame.BoardGameDescription,
                        boardGameBuyUrl = boardGame.BoardGameBuyUrl,
                        boardGameGenre = boardGame.BoardGameGenre
                    }
                );
            };
                return Ok(boardGamesDtoList);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return BadRequest();
            }


        }



        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromBody] string newBoardGameName)
        {
            
            var contentModerator = new ContentModerator(newBoardGameName);
            string outputString = await contentModerator.GetModeratedString();
            
            if (outputString == "Input string was considered too toxic to output")
            {
                return BadRequest("Search string was considered too inflammatory to process...");
            } 

            var aiGameResearcher = new AIGameResearcher(outputString,gamesRepository);

            var boardGameResearchObject = await aiGameResearcher.GetBoardGameResearchObj();
                        
            if (boardGameResearchObject is RealBoardGameDto)
            {

                var boardGame = new BoardGame()
                {
                    BoardGameId = boardGameResearchObject.boardGameId,
                    BoardGameName = boardGameResearchObject.boardGameName,
                    BoardGameDescription = boardGameResearchObject.boardGameDescription,
                    BoardGameBuyUrl = boardGameResearchObject.boardGameBuyUrl,
                    BoardGameGenre = boardGameResearchObject.boardGameGenre
                };
                
                var boardGameAddedToDatabase = await gamesRepository.CreateAsync(boardGame);

                return Ok(boardGameAddedToDatabase);
            }
            else
            {
                return BadRequest("No board game exists with this name or this boardgame has already been added to database");
            }
        
        
        }













    }









}