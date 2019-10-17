using System;
using Test.Shared;
using Microsoft.Extensions.Logging;
using Test.Data.DataAccessObjects.MSSql;
using CommandLine;

namespace Test.Utility.Actions
{
    [Verb("drop", HelpText = "Drop the DB")]
    class DropOptions { }
    
    public class Drop
    {
        public static int Run(ILogger logger)
        {
            try
            {
                logger.LogInformation($"Try to drop \"{AppSettings.DatabaseName}\" database");

                using (var context = new DatabaseContext(AppSettings.MSSqlServerConnectionString))
                {
                    context.Database.EnsureDeleted();
                }

                logger.LogInformation($"{AppSettings.DatabaseName} database successfully dropped");
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