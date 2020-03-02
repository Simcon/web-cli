using System;

namespace WebCLI.Commands.Twitter.Client
{
    public class Tweet
    {
        public string Id { get; }
        public Uri Uri { get; }
        public string Name { get; }
        public string Handle { get; }
        public DateTime Date { get; }
        public string Text { get; }

        public Tweet(string id, Uri uri, string name, string handle, DateTime date, string text)
        {
            Id = id;
            Uri = uri;
            Name = name;
            Handle = handle;
            Date = date;
            Text = text;
        }
    }
}
