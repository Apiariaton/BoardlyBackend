using System.ComponentModel.DataAnnotations;

namespace CSharpBackend.API.Models.DataTransferObjects 
{

    public class PlayerVsPlayerDuelDto
    {
        [Required]
        [Range(0,999)]
        public int playerAttacksPerDuel { get; set;}

        [Required]
        [Range(0,999)]
        public int opponentAttacksPerDuel {get; set;}

        [Required]
        [Range(0,3600)]
        public int duelDurationInSeconds {get;set;}

        [Required]
        public bool playerWonThisDuel {get;set;}

        public string chosenBoardGame {get; set;}

        public string chosenBoardGameGenre {get;set;}

    }






}