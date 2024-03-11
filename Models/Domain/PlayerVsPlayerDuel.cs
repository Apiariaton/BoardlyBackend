namespace CSharpBackend.API.Models.Domain 
{

    public class PlayerVsPlayerDuel
    {

        public Guid DuelId {get;set;}

        public int playerAttacksPerDuel { get; set;}

        public int opponentAttacksPerDuel {get; set;}

        public int duelDurationInSeconds {get;set;}

        public bool playerWonThisDuel {get;set;}

        public string chosenBoardGame {get; set;}

        public string chosenBoardGameGenre {get;set;}

    }





}