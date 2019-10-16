using System;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Test.Data.DataAccessObjects.MSSql;
using Test.Shared;
using Test.Utility.Actions;

namespace Test.Utility
{
    [Verb("app-settings", HelpText = "Set application settings by it names")]
    class ApplicationSettingsOptions
    {
        [Option(AppSettings.DatabaseHostVariableName, HelpText = "Allow to set database host")]
        public string DatabaseHost { get; set; }

        [Option(AppSettings.DatabaseNameVariableName, HelpText = "Allow to set database name")]
        public string DatabaseName { get; set; }

        [Option(AppSettings.DatabasePortVariableName, HelpText = "Allow to set database port")]
        public string DatabasePort { get; set; }

        [Option(AppSettings.DatabaseUserNameVariableName, HelpText = "Allow to set database user name")]
        public string DatabaseUserName { get; set; }

        [Option(AppSettings.DatabasePasswordVariableName, HelpText = "Allow to set database password")]
        public string DatabasePassword { get; set; }
    }

    [Verb("migrate", HelpText = "Migrate the DB schema to the latest version")]
    class MigrateOptions { }

    [Verb("create", HelpText = "Create the DB")]
    class CreateOptions { }

    [Verb("reset", HelpText = "Reset the DB (drop, create, migrate, seed)")]
    class ResetOptions { }

    [Verb("drop", HelpText = "Drop the DB")]
    class DropOptions { }
    class Program
    {
        static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return CommandLine.Parser.Default.ParseArguments<MigrateOptions, DropOptions, CreateOptions, ResetOptions, ApplicationSettingsOptions>(args)
            .MapResult(
                (DropOptions options) => RunDrop(serviceProvider.GetService<ILogger<Drop>>(), options),
                (ResetOptions options) => RunReset(serviceProvider.GetService<ILogger<Reset>>(), options),
                (CreateOptions options) => RunCreate(serviceProvider.GetService<ILogger<Create>>(), options),
                (MigrateOptions options) => RunMigrate(serviceProvider.GetService<ILogger<Migrate>>(), options),
                (ApplicationSettingsOptions options) => RunSettingsUpdate(serviceProvider.GetService<ILogger<SettingsUpdate>>(), options),
                errs => 1
            );
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole());
        }

        static int RunDrop(ILogger logger, DropOptions options) => Drop.Run(logger);
        static int RunReset(ILogger logger, ResetOptions options) => Reset.Run(logger);
        static int RunCreate(ILogger logger, CreateOptions options) => Create.Run(logger);
        static int RunMigrate(ILogger logger, MigrateOptions options) => Migrate.Run(logger);
        static int RunSettingsUpdate(ILogger logger, ApplicationSettingsOptions options) => SettingsUpdate.Run(logger, options.DatabaseHost, options.DatabaseName, options.DatabasePort, options.DatabaseUserName, options.DatabasePassword);
    }
}
