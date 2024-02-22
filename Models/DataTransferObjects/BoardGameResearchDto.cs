namespace CSharpBackend.API.Models.DataTransferObjects 
{


    public class BoardGameResearchDto
    {


        public bool boardGameExists {get; set;}
        public string boardGameName { get; set;}

        public string boardGameDescription {get; set;}

        public float boardGamePrice {get; set;}

        public string boardGameBuyUrl {get;set;}

        public string boardGameGenre {get;set;}



    }

}