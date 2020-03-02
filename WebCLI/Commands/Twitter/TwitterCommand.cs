using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using WebCLI.Commands.Twitter.Client;

namespace WebCLI.Commands.Twitter
{
    public class TwitterCommand : Command
    {
        private const string timeline = @"i/timeline";
        private const string trends = @"i/trends";

        public TwitterCommand(string name, TwitterClient client) : base(name)
        {
            Description = "Description of TwitterCommand";

            AddOption(new Option(
                new[] { "--json" },
                "Return as Json")
            {
                Argument = new Argument<bool>() // defaultValue: () => 99
                {
                    //Arity = ArgumentArity.ExactlyOne
                },
                Description = "Argument description"
            });

            Handler = CommandHandler.Create<bool>(async (json) =>
            {
                var output = await client.GetTweets(timeline);
                var text = json ? output.Json : output.String;
                ConsoleHelper.WriteLine(text);
                Environment.ExitCode = (int)ExitCode.Success;
            });
        }
    }
}
