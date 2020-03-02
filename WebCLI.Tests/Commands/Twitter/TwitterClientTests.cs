using ApprovalTests;
using ApprovalTests.Namers;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCLI.Commands.Twitter.Client;

namespace WebCLI.Tests.Commands.Twitter
{
    public class TwitterClientTests : TestBase
    {
        [Test]
        public async Task ClientTest([ValueSource("_files")] string file)
        {
            var response = ReadFile(file);
            var client = new TwitterClient(GetFakeHttpClientFactory(response));

            using (ApprovalResults.ForScenario(file))
            {
                var output = await client.GetTweets(string.Empty);
                Approvals.Verify(output.Json);
            }
        }
    }
}
