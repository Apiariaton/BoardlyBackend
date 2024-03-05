namespace CSharpBackend.API.ModeratorClasses
{


    public class ContentModeratorRequest
    {
        public Comment comment {get;set;}

        public string[] languages {get;set;}

        public RequestedAttributes requestedAttributes {get;set;}

    }


    public class Comment
    {
        public string text {get;set;} 
    }

    public class RequestedAttributes
    {
        public object TOXICITY {get;set;}
    }



}