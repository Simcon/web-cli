using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using WebCLI.Commands.Client;
using WebCLI.Commands.HackerNews;
using WebCLI.Commands.Test;
using WebCLI.Commands.Twitter;
using WebCLI.Commands.Twitter.Client;

namespace WebCLI
{
    static class Program
    {
        private static IServiceProvider services;
        private static RootCommand rootCommand;

        static void Main(string[] args)
        {
            ConfigureServices();
            ConfigureCommands();

            rootCommand.Invoke(args);

            DisposeServices();
        }

        static void ConfigureCommands()
        {
            rootCommand = new RootCommand()
            {
                new TestCommand("test"),
                new TwitterCommand("twitter", services.GetService<TwitterClient>())
                {
                    new Commands.Twitter.Verbs.ListCommand("list", services.GetService<TwitterClient>())
                },
                new HackerNewsCommand("hackernews", services.GetService<HackerNewsClient>())
            };
        }

        private static IConfiguration Configuration;

        private static void Configure()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.Development.json", optional: true)
               .AddJsonFile("appsettings.json", optional: true);
            Configuration = builder.Build();
        }

        static void ConfigureServices()
        {
            var collection = new ServiceCollection();
            collection.AddHttpClient();
            collection.AddTransient<TwitterClient>();
            collection.AddTransient<HackerNewsClient>();
            services = collection.BuildServiceProvider();
        }

        static void DisposeServices()
        {
            if (services == null)
            {
                return;
            }
            if (services is IDisposable)
            {
                ((IDisposable)services).Dispose();
            }
        }
    }
}
