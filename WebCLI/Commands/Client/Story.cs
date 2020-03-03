using System;

namespace WebCLI.Commands.HackerNews.Client
{
    public class Story
    {
        public string Id { get; }
        public Uri Uri { get; }
        public string Title { get; }

        public Story(string id, Uri uri, string title)
        {
            Id = id;
            Uri = uri;
            Title = title;
        }
    }
}
