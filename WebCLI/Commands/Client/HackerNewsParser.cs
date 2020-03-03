using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;

namespace WebCLI.Commands.HackerNews.Client
{
    internal class HackerNewsParser
    {
        private static IHtmlDocument _document;
        private static IHtmlCollection<IElement> _stories;

        public static HackerNewsOutput MapResults(string html)
        {
            ReadHtml(html);
            GetNews();
            return MapToOutput();

            static HackerNewsOutput MapToOutput()
            {
                var mappedTweets = new List<Story>();

                foreach (var story in _stories)
                {
                    var id = GetId(story);
                    var uri = GetUri(story);
                    var title = GetTitle(story);
                    mappedTweets.Add(new Story(id, uri, title));
                }

                return new HackerNewsOutput(mappedTweets);
            }
        }

        private static void ReadHtml(string html)
        {
            _document = new HtmlParser().ParseDocumentAsync(html).Result;
        }

        private static void GetNews()
        {
            var selector = ".athing";
            _stories = _document.QuerySelectorAll(selector);
        }

        private static string GetId(IElement tweet)
        {
            return tweet.GetAttribute("id");
        }

        private static Uri GetUri(IElement story)
        {
            var href = story.QuerySelector(".storylink").GetAttribute("href");

            if (!href.StartsWith("http"))
            {
                return new Uri($"https://news.ycombinator.com/{href}");
            }
            else
            {
                return new Uri($"{href}");
            }
        }

        private static string GetTitle(IElement story)
        {
            return story.QuerySelector(".storylink").TextContent;
        }
    }
}
