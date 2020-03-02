using FluentAssertions;
using NUnit.Framework;
using System;

namespace WebCLI.SystemTests.Commands.Twitter
{
    public class TwitterCommandTests
    {
        [Test]
        public void ListCommandTest()
        {
            var args = "twitter";

            var result = ProcessHelper.StartConsoleApplication(args);

            result.Error.Should().Be(string.Empty);
            result.ExitCode.Should().Be(0);
            result.Output.Should().Be("\n");
        }
    }
}
