using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Text;


namespace CSharpBackend.API.ModeratorClasses
{

    public class ContentModerator
    {
       
        private readonly string inputString;

        private HttpClient httpClient;

        private readonly float MaxToxicityScore;

        private readonly string[] LanguageCodes;

        public ContentModerator(string inputString)
        {
            this.inputString = HttpUtility.HtmlEncode(inputString);
            this.httpClient = new HttpClient();
            
            this.httpClient.DefaultRequestHeaders.Add("Content-Type","application/json");
            

            try
            {
            string JSONSettingsContent = File.ReadAllText(@"ModeratorClasses\contentmoderationsettings.json");
            var moderatorSettings = JsonSerializer.Deserialize<ContentModerationSettings>(JSONSettingsContent);
            
            MaxToxicityScore = moderatorSettings.MaxToxicityScore;
            LanguageCodes = moderatorSettings.LanguageCodes;


            var perspectiveAPIKey = Environment.GetEnvironmentVariable("PERSPECTIVE_API_Key",EnvironmentVariableTarget.User);                
            this.httpClient.BaseAddress = new Uri(moderatorSettings.BaseAddress + "?key=" + perspectiveAPIKey);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Please ensure that the file path to contentmoderationsettings.json is ModeratorClasses/contentmoderationsettings.json");
                throw;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Please ensure that inside the Moderator Classes, a file exists with the name contentmoderationsettings.json");
                throw;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("contentmoderationsettings.json and the environment key must be initialised with correct values...");
                throw;
            }

        }

        public async Task<string> GetModeratedString()
        {
            try {

                var moderatedString = await TryToGetModeratedString();
                return moderatedString;
            }
            catch () {

            
            }
            catch ()
            {


            }
        }


        private async Task<string> TryToGetModeratedString()
        {

            var moderationContent = CreateHttpContentForHttpClient();

            var contentModeratorResponse = await httpClient.PostAsync(moderationContent);
            


        }





        private HttpContent CreateHttpContentForHttpClient()
        {

            try 
            {
                var JSONRequestString = CreateRawJSONRequestString();
                HttpContent HttpRequestContent = TurnRawJSONToHttpContent(JSONRequestString);
                return HttpRequestContent;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Please ensure that contentModeratorRequest is a valid object which can be transformed into JSON");
                throw;

            }
        }


        private string CreateRawJSONRequestString()
        {

            var contentModeratorRequest = new ContentModeratorRequest
            {
                comment = new Comment {text = inputString},
                languages = LanguageCodes,
                requestedAttributes = new RequestedAttributes { TOXICITY = new object() }
            };

            string rawJSONRequestString = JsonSerializer.Serialize(contentModeratorRequest);

            return rawJSONRequestString;

        }


        private HttpContent TurnRawJSONToHttpContent(string rawJSONRequestString)
        {

            return new StringContent(rawJSONRequestString,Encoding.UTF8,"application/json");

        }
    


    }










}