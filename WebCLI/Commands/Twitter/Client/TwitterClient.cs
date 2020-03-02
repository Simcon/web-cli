using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebCLI.Commands.Twitter.Client
{
    public class TwitterClient
    {
        private readonly IHttpClientFactory _httpFactory;

        public TwitterClient(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<TwitterOutput> GetTweets(string username, bool debug = false)
        {
            var uri = $"https://twitter.com/{username}";

            using (HttpClient httpclient = _httpFactory.CreateClient())
            {
                using (HttpResponseMessage response = await httpclient.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    if (debug)
                    {
                        await File.WriteAllTextAsync($@"D:\GitHub\web-cli\WebCLI\bin\Release\netcoreapp3.1\win10-x64\publish\{username}_debug.html", result);
                    }

                    return TwitterParser.MapResults(result);
                }
            }
        }
    }
}
