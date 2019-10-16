using System;
using Test.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Test.Data.DataAccessObjects.MSSql;
using Microsoft.EntityFrameworkCore;

namespace Test.Utility.Actions
{
    public class Migrate
    {
        public static int Run(ILogger logger)
        {
            try
            {
                logger.LogInformation($"Start migrating the {AppSettings.DatabaseName} database");

                using(var context = new DatabaseContext(AppSettings.MSSqlServerConnectionString))
                {
                    context.Database.Migrate();
                }

                logger.LogInformation($"{AppSettings.DatabaseName} database successfully migrated");
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