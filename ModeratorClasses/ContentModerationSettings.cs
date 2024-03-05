namespace CSharpBackend.API.ModeratorClasses
{

    public class ContentModerationSettings
    {

        public float MaxToxicityScore {get;set;}

        public string[] LanguageCodes {get;set;}

        public string BaseAddress {get;set;}
    }


}