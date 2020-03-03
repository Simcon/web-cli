using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WebCLI.Commands.HackerNews.Client;

namespace WebCLI.Commands.Client
{
    public class HackerNewsClient
    {
        private readonly IHttpClientFactory _httpFactory;

        public HackerNewsClient(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<HackerNewsOutput> GetFrontpage(bool debug = false)
        {
            var uri = $"https://news.ycombinator.com/";

            using (HttpClient httpclient = _httpFactory.CreateClient())
            {
                using (HttpResponseMessage response = await httpclient.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    if (debug)
                    {
                        await File.WriteAllTextAsync($@"D:\GitHub\web-cli\WebCLI\bin\Release\netcoreapp3.1\win10-x64\publish\hackernews_frontpage_debug.html", result);
                    }

                    return HackerNewsParser.MapResults(result);
                }
            }
        }
    }
}
