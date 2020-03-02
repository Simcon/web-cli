using FluentAssertions;
using NUnit.Framework;

namespace WebCLI.SystemTests.Commands.Twitter.Verbs
{
    public class ListCommandTests
    {
        [Test]
        public void ListCommandTest()
        {
            var args = "twitter list -u potus --json";

            var result = ProcessHelper.StartConsoleApplication(args);

            result.Error.Should().Be(string.Empty);
            result.ExitCode.Should().Be(0);
            // TODO: parse the output and count the tweets.
            // Deserialization currently broken due to quotes in the output. Needs escaping.
        }
    }
}
