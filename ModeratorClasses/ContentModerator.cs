using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace CSharpBackend.API.ModeratorClasses
{

    public class ContentModerator
    {
       
        private readonly string inputString;

        private HttpClient httpClient;

        private readonly int MaxToxicityScore;

        private readonly string[] LanguageCodes;

        public ContentModerator(string inputString)
        {
            this.inputString = HttpUtility.HtmlEncode(inputString);
            this.httpClient = new HttpClient();
                        
            try
            {
            string JSONSettingsContent = File.ReadAllText(@"ModeratorClasses\contentmoderationsettings.json");
            var moderatorSettings = JsonConvert.DeserializeObject<ContentModerationSettings>(JSONSettingsContent);
            
            MaxToxicityScore = int.Parse(moderatorSettings.MaxToxicityScore);
            LanguageCodes = moderatorSettings.LanguageCodes;


            var perspectiveAPIKey = Environment.GetEnvironmentVariable("PERSPECTIVE_API_KEY",EnvironmentVariableTarget.User);                
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
            catch (NotSupportedException) {
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine("An issue occurred - causes may include: internet connection, incorrect API or URL");
                throw;
            }
        }
        private async Task<string> TryToGetModeratedString()
        {

            HttpContent moderationContent = CreateRequestHttpContent();
            HttpResponseMessage contentModeratorResponse = await SendContentToModeratorAPI(moderationContent);
            ContentModerationResponse contentModerationResponseBody = await DeserializeContentModeratorResponse(contentModeratorResponse);
        
            double contentToxicityScore = GetContentToxicityScore(contentModerationResponseBody);
            string moderatedInputString = GetModerationResult(contentToxicityScore);

            return moderatedInputString;

        }




        private HttpContent CreateRequestHttpContent()
        {

            try 
            {
                var requestAsJSONString = CreateRequestJSONString();
                HttpContent RequestAsHttpContent = CreateRequestHttpContent(requestAsJSONString);
                return RequestAsHttpContent;
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Please ensure that contentModeratorRequest is a valid object which can be transformed into JSON");
                throw;

            }
        }

        private async Task<HttpResponseMessage> SendContentToModeratorAPI(HttpContent moderationContent)
        {
            var contentModeratorResponse = await httpClient.PostAsync(this.httpClient.BaseAddress,moderationContent);
            contentModeratorResponse.EnsureSuccessStatusCode();
            return contentModeratorResponse;
        }


        private async Task<ContentModerationResponse> DeserializeContentModeratorResponse(HttpResponseMessage contentModeratorResponse)
        {
            string contentModeratorResponseString = await contentModeratorResponse.Content.ReadAsStringAsync();
            
            ContentModerationResponse contentModerationResponseBody = JsonConvert.DeserializeObject<ContentModerationResponse>(contentModeratorResponseString);

            return contentModerationResponseBody;
        }


        private double GetContentToxicityScore(ContentModerationResponse contentModerationResponseBody)
        {
            return contentModerationResponseBody.attributeScores.Toxicity.summaryScore.value;
        }

        private string GetModerationResult(double contentToxicityScore)
        {
            return contentToxicityScore > MaxToxicityScore ? "Input string was considered too toxic to output" :  inputString;
        }

        




        private string CreateRequestJSONString()
        {

            var contentModeratorRequest = new ContentModeratorRequest
            {
                comment = new Comment {text = inputString},
                languages = LanguageCodes,
                requestedAttributes = new RequestedAttributes { TOXICITY = new object() }
            };

            string rawJSONRequestString = JsonConvert.SerializeObject(contentModeratorRequest);

            return rawJSONRequestString;

        }

        private HttpContent CreateRequestHttpContent(string rawJSONRequestString)
        {

            return new StringContent(rawJSONRequestString,Encoding.UTF8,"application/json");

        }


  





  
    


    }










}