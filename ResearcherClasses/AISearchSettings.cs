using System.IO;
using System.Text.Json; 


namespace CSharpBackend.API.ResearcherClasses
{

    public class AISearchSettings
    {

        public string deploymentName {get;set;}
        public string chatRequestUserMessage {get;set;}

        public string chatRequestSystemMessage {get;set;}

    }



}