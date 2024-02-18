namespace CSharpBackend.API.Models.Domain 
{

    public class Match
    {

        public Guid MatchId {get;set;}

        public float playerAttacksPerRound { get; set;}

        public float opponentAttacksPerRound {get; set;}

        public int matchDurationInSeconds {get;set;}

        public string chosenBoardGame {get; set;}

        public string chosenBoardGameGenre {get;set;}

    }





}