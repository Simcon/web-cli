using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using WebCLI.Code.Extensions;

namespace WebCLI.Commands.Test
{
    public class TestCommand : Command
    {
        public TestCommand(string name) : base(name)
        {
            Description = "Description of TestCommand";

            AddOption(new Option(
                new[] { "--int-option", "-io" },
                "An option whose argument is parsed as an int")
            {
                Argument = new Argument<int>(defaultValue: () => 42)
                {
                    //Arity = ArgumentArity.ExactlyOne
                },
                Description = "Argument description"
            });

            Handler = CommandHandler.Create<int>((intOption) =>
            {
                ConsoleHelper.WriteLine($"The value for --int-option is: {intOption}");
                Environment.ExitCode = (int)ExitCode.Success;
            });
        }
    }
}
