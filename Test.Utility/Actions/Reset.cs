using System;
using CommandLine;
using Microsoft.Extensions.Logging;
using Test.Shared;

namespace Test.Utility.Actions
{
    [Verb("reset", HelpText = "Reset the DB (drop, create, migrate, seed)")]
    public class ResetOptions : ApplicationSettingsOptions { }

    public class Reset
    {
        public static int Run(ILogger logger, ResetOptions options)
        {
            try
            {
                logger.LogInformation($"Try to reset \"{AppSettings.DatabaseName}\" database");

                SettingsUpdate.Run(logger, options);//@"localhost\sqlexpress", "Test", "5432", "sysad", "admin");
                if(Drop.Run(logger) > 0) throw new Exception("There was some errors with dropping database");
                if(Create.Run(logger) > 0) throw new Exception("There was some errors with creating database");
                if(Migrate.Run(logger) > 0) throw new Exception("There was some errors with migrating database");

                logger.LogInformation($"{AppSettings.DatabaseName} database successfully reseted");
                return 0;
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
                return 1;
            }
        }
    }
}