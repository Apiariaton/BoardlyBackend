namespace CSharpBackend.API.Models.DataTransferObjects 
{

    public abstract class  BoardGameDto
    {

        public Guid boardGameId {get;set;}

        public virtual string boardGameName { get; set;}

        public virtual string boardGameDescription {get; set;}

        public virtual string boardGameBuyUrl {get;set;}

        public virtual string boardGameGenre {get;set;}


    }

    public class RealBoardGameDto : BoardGameDto
    {

        public override string boardGameName { get; set;}

        public override string boardGameDescription {get; set;}

        public override string boardGameBuyUrl {get;set;}

        public override string boardGameGenre {get;set;}

    }


    public class NullBoardGameDto : BoardGameDto
    {

        public NullBoardGameDto()
        {
            boardGameName = "NullBoardGame";
        }

        public override string boardGameDescription {get; set;}

        public override string boardGameBuyUrl {get;set;}

        public override string boardGameGenre {get;set;}

    }





}