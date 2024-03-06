namespace CSharpBackend.API.ModeratorClasses
{

    public class ContentModerationSettings
    {

        public string MaxToxicityScore {get;set;}

        public string[] LanguageCodes {get;set;}

        public string BaseAddress {get;set;}
    }


}