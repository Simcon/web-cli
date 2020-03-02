using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using WebCLI.Commands.Twitter.Client;

namespace WebCLI.Commands.Twitter.Verbs
{
    public class ListCommand : BaseCommand
    {
        public ListCommand(string name, TwitterClient client) : base(name)
        {
            Description = "Description of ListCommand";

            AddOption(new Option(
                new[] { "--username", "-u" }//, "User Id to list"
                )
            {
                Argument = new Argument<string>()
                {
                    //Arity = ArgumentArity.ExactlyOne
                },
                Description = "Argument description",
                //Required = true
            });

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

            Handler = CommandHandler.Create<string, bool, bool>(async (username, json, debug) =>
            {
                var output = await client.GetTweets(username, debug);
                var text = json ? output.Json : output.String;
                ConsoleHelper.WriteLine(text);
                Environment.ExitCode = (int)ExitCode.Success;
            });
        }
    }
}
