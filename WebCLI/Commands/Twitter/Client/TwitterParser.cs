using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCLI.Commands.Twitter.Client
{
    internal class TwitterParser
    {
        private static IHtmlDocument _document;
        private static IHtmlCollection<IElement> _tweets;

        public static TwitterOutput MapResults(string html)
        {
            ReadHtml(html);
            GetTweets();
            return MapToOutput();
        }

        private static void ReadHtml(string html)
        {
            _document = new HtmlParser().ParseDocumentAsync(html).Result;
        }

        private static void GetTweets()
        {
            var tweetSelector = ".stream-items .tweet";
            _tweets = _document.QuerySelectorAll(tweetSelector);
        }

        private static TwitterOutput MapToOutput()
        {
            var mappedTweets = new List<Tweet>();

            foreach (var tweet in _tweets)
            {
                var id = GetId(tweet);
                var uri = GetUri(tweet);
                var name = GetName(tweet);
                var handle = GetHandle(tweet);
                var date = GetDate(tweet);
                var text = GetText(tweet);
                mappedTweets.Add(new Tweet(id, uri, name, handle, date, text));
            }

            return new TwitterOutput(mappedTweets);
        }

        private static string GetId(IElement tweet)
        {
            var href = tweet.QuerySelector(".tweet-timestamp").GetAttribute("href");
            return href.Split('/').Last();
        }

        private static Uri GetUri(IElement tweet)
        {
            var href = tweet.QuerySelector(".tweet-timestamp").GetAttribute("href");
            return new Uri($"https://twitter.com{href}");
        }

        private static string GetName(IElement tweet)
        {
            return tweet.QuerySelector(".fullname").TextContent;
        }

        private static string GetHandle(IElement tweet)
        {
            return tweet.QuerySelector(".username b").TextContent;
        }

        public static DateTime GetDate(IElement tweet)
        {
            var tweetDate = tweet.QuerySelector("._timestamp").GetAttribute("data-time");
            long.TryParse(tweetDate, out long unixSeconds);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixSeconds);
            return dateTimeOffset.UtcDateTime;
        }

        private static string GetText(IElement tweet)
        {
            return tweet.QuerySelector(".tweet-text").TextContent;
        }
    }
}
