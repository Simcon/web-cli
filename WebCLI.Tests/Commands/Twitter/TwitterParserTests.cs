using ApprovalTests;
using ApprovalTests.Namers;
using NUnit.Framework;
using WebCLI.Commands.Twitter.Client;

namespace WebCLI.Tests.Commands.Twitter
{
    public class TwitterParserTests : TestBase
    {
        [Test]
        public void ParseJsonTests([ValueSource("_files")] string file)
        {
            var html = ReadFile(file);
            using (ApprovalResults.ForScenario(file))
            {
                var output = TwitterParser.MapResults(html);
                Approvals.Verify(output.Json);
            }
        }

        [Test]
        public void ParseReadableTests([ValueSource("_files")] string file)
        {
            var html = ReadFile(file);
            using (ApprovalResults.ForScenario(file))
            {
                var output = TwitterParser.MapResults(html);
                Approvals.Verify(output.String);
            }
        }
    }
}
