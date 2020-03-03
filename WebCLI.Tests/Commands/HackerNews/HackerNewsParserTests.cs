using ApprovalTests;
using ApprovalTests.Namers;
using NUnit.Framework;
using WebCLI.Commands.HackerNews.Client;

namespace WebCLI.Tests.Commands.HackerNews
{
    public class HackerNewsParserTests : HackerNewsTestBase
    {
        [Test]
        public void ParseJsonTests([ValueSource("_files")] string file)
        {
            var html = ReadFile(file);
            using (ApprovalResults.ForScenario(file))
            {
                var output = HackerNewsParser.MapResults(html);
                Approvals.Verify(output.Json);
            }
        }

        [Test]
        public void ParseReadableTests([ValueSource("_files")] string file)
        {
            var html = ReadFile(file);
            using (ApprovalResults.ForScenario(file))
            {
                var output = HackerNewsParser.MapResults(html);
                Approvals.Verify(output.String);
            }
        }
    }
}
