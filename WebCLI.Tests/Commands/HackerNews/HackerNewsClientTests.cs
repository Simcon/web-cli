using ApprovalTests;
using ApprovalTests.Namers;
using NUnit.Framework;
using System.Threading.Tasks;
using WebCLI.Commands.Client;

namespace WebCLI.Tests.Commands.HackerNews
{
    public class HackerNewsClientTests : HackerNewsTestBase
    {
        [Test]
        public async Task ClientTest([ValueSource("_files")] string file)
        {
            var response = ReadFile(file);
            var client = new HackerNewsClient(GetFakeHttpClientFactory(response));

            using (ApprovalResults.ForScenario(file))
            {
                var output = await client.GetFrontpage();
                Approvals.Verify(output.Json);
            }
        }
    }
}
