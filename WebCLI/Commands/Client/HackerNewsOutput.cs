using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WebCLI.Code.Extensions;

namespace WebCLI.Commands.HackerNews.Client
{
    public class HackerNewsOutput : Output
    {
        private ReadOnlyCollection<Story> _stories;

        public override string String
        {
            get
            {
                var sb = new StringBuilder();
                _stories.ToList().ForEach(story => sb.AppendLine($"{story.Title}"));
                return sb.ToString();
            }
        }

        public override string Json
        {
            get
            {
                return _stories.ToJson(Newtonsoft.Json.Formatting.Indented);
            }
        }

        public HackerNewsOutput(IEnumerable<Story> stories)
        {
            _stories = new ReadOnlyCollection<Story>(stories.ToList());
        }
    }
}
