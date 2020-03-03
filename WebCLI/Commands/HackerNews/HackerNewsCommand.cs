using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using WebCLI.Commands.Client;

namespace WebCLI.Commands.HackerNews
{
    public class HackerNewsCommand : BaseCommand
    {
        public HackerNewsCommand(string name, HackerNewsClient client) : base(name)
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

            Handler = CommandHandler.Create<bool, bool>(async (json, debug) =>
            {
                var output = await client.GetFrontpage(debug);
                var text = json ? output.Json : output.String;
                ConsoleHelper.WriteLine(text);
                Environment.ExitCode = (int)ExitCode.Success;
            });
        }
    }
}
