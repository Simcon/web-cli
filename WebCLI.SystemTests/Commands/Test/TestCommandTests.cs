using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;
using WebCLI.Code.Extensions;

namespace WebCLI.SystemTests.Commands.Test
{
    [UseApprovalSubdirectory("approvals")]
    [UseReporter(typeof(DiffReporter))]
    public class TestCommandTests
    {
        [Test]
        public void TestWithoutOption()
        {
            var args = "test";

            var result = ProcessHelper.StartConsoleApplication(args);

            Approvals.Verify(result.ToJson(Newtonsoft.Json.Formatting.Indented));
        }

        [Test]
        public void TestWithOption()
        {
            var args = "test -io 23";

            var result = ProcessHelper.StartConsoleApplication(args);

            Approvals.Verify(result.ToJson(Newtonsoft.Json.Formatting.Indented));
        }
    }
}
