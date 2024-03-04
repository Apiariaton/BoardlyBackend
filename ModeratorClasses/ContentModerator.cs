using System.Web;
using System.Net.Http;
using System.Text.Json;
using System.IO;


namespace CSharpBackend.API.ModeratorClasses
{

    public class ContentModerator
    {
       
        private readonly string inputString;

        private HttpClient httpClient;

        private readonly float MaxToxicityScore;

        private readonly string LanguageCode;

        public ContentModerator(string inputString)
        {
            this.inputString = HttpUtility.HtmlEncode(inputString);
            this.httpClient = new HttpClient();

            string JSONSettingsContent = File.ReadLines(@"ModeratorClasses\contentmoderationsettings.json");
            var moderatorSettings = JsonSerializer.Deserialize<ContentModerationSettings>(JSONSettingsContent);


        }

        public Task<string> GetModeratedString()
        {
            try {

                var moderatedString = await TryToGetModeratedString(inputString);
                return moderatedString;
            }
            catch () {



            }
            catch ()
            {






            }
        }


        public Task<string> TryToGetModeratedString()
        {









        }
    


    }










}