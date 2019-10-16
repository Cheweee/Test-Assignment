using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Test.Data.DataAccessObjects.MSSql;
using Test.Shared;

namespace Test.Utility.Actions
{
    public class Create
    {
        public static int Run(ILogger logger)
        {
            try
            {
                logger.LogInformation($"Try to create \"{AppSettings.DatabaseName}\" database");
                
                using(var context = new DatabaseContext(AppSettings.MSSqlServerConnectionString))
                {
                    context.Database.EnsureCreated();
                }

                logger.LogInformation($"{AppSettings.DatabaseName} database successfully created");
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