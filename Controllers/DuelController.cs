using CSharpBackend.API.Data;
using CSharpBackend.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using CSharpBackend.API.Models.DataTransferObjects;
using CSharpBackend.API.Models.Domain;




namespace CSharpBackend.API.Controllers{



    [Route("api/[controller]")]
    [ApiController]

    public class DuelController : ControllerBase
    {

        private readonly CSharpBackendDBContext dbContext;
        private readonly IDuelRepository playerVsPlayerDuelRepository;


        public DuelController(CSharpBackendDBContext dbContext, IDuelRepository playerVsPlayerDuelRepository)
        {
            this.dbContext = dbContext;
            this.playerVsPlayerDuelRepository = playerVsPlayerDuelRepository;
        }



        public async Task<IActionResult> CreateAsync([FromBody] PlayerVsPlayerDuelDto playerVsPlayerDuelDto)
        {
            if (ModelState.IsValid)
            {

                var matchCreatedFromPostRequest = new PlayerVsPlayerDuel {
                    DuelId = Guid.NewGuid(),
                    playerAttacksPerDuel = playerVsPlayerDuelDto.playerAttacksPerDuel,
                    opponentAttacksPerDuel = playerVsPlayerDuelDto.opponentAttacksPerDuel,
                    duelDurationInSeconds = playerVsPlayerDuelDto.duelDurationInSeconds,
                    playerWonThisDuel = playerVsPlayerDuelDto.playerWonThisDuel,
                    chosenBoardGame = playerVsPlayerDuelDto.chosenBoardGame,
                    chosenBoardGameGenre = playerVsPlayerDuelDto.chosenBoardGameGenre
                };


            }
            else
            {

                return BadRequest("The duel data was sent to this controller with data fields that were incomplete or incorrect");

            }
        }















    }













































}