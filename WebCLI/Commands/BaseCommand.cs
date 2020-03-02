using System.CommandLine;

namespace WebCLI.Commands
{
    public class BaseCommand : Command
    {
        public BaseCommand(string name) : base(name)
        {
            AddOption(new Option(
            new[] { "--debug" },
            "Write html to file")
            {
                Argument = new Argument<bool>(),
                Description = "Argument description"
            });
        }
    }
}
