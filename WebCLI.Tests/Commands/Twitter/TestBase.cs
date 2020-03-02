using ApprovalTests.Namers;
using NSubstitute;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace WebCLI.Tests.Commands.Twitter
{
    [UseApprovalSubdirectory("approvals")]
    public class TestBase
    {
        protected static readonly string[] _files = new[]
        {
            "lfc.html",
            "potus.html",
            "elonmusk.html"
        };

        protected string ReadFile(string filename, [CallerFilePath] string callerpath = "")
        {
            var location = Path.GetDirectoryName(callerpath);

            if(location == null)
            {
                throw new NullReferenceException();
            }

            var sample = Path.Combine(location, "files", filename);
            return File.ReadAllText(sample);
        }

        protected IHttpClientFactory GetFakeHttpClientFactory(string response)
        {
            var fakeHttpMessageHandler = GetFakeHttpMessageHandler(response);
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);
            return httpClientFactoryMock;
        }   

        protected FakeHttpMessageHandler GetFakeHttpMessageHandler(string response)
        {
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(response)
            });
            return fakeHttpMessageHandler;
        }
    }
}
