using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WebCLI.Code.Extensions;

namespace WebCLI.Commands.Twitter.Client
{
    public class TwitterOutput : Output
    {
        private ReadOnlyCollection<Tweet> _tweets;

        public override string String
        {
            get
            {
                var sb = new StringBuilder();
                _tweets.ToList().ForEach(tweet => sb.AppendLine($"{tweet.Name + $" ({tweet.Handle})", -38}\t{tweet.Text}"));
                return sb.ToString();
            }
        }

        public override string Json
        {
            get
            {
                return _tweets.ToJson(Newtonsoft.Json.Formatting.Indented);
            }
        }

        public TwitterOutput(IEnumerable<Tweet> tweets)
        {
            _tweets = new ReadOnlyCollection<Tweet>(tweets.ToList());
        }
    }
}
