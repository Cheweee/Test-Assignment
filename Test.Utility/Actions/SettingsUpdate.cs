using System;
using Microsoft.Extensions.Logging;
using Test.Shared;

namespace Test.Utility.Actions
{
    public class SettingsUpdate
    {
        public static int Run(
            ILogger logger,
            string DatabaseHost = null,
            string DatabaseName = null,
            string DatabasePort = null,
            string DatabaseUserName = null,
            string DatabasePassword = null
        )
        {
            try
            {
                logger.LogInformation("Try to update application settings");
                if (!string.IsNullOrEmpty(DatabaseHost))
                    AppSettings.SetAppSetting(AppSettings.DatabaseHostVariableName, DatabaseHost);

                if (!string.IsNullOrEmpty(DatabaseName))
                    AppSettings.SetAppSetting(AppSettings.DatabaseNameVariableName, DatabaseName);

                if (!string.IsNullOrEmpty(DatabasePort))
                    AppSettings.SetAppSetting(AppSettings.DatabasePortVariableName, DatabasePort);

                if (!string.IsNullOrEmpty(DatabaseUserName))
                    AppSettings.SetAppSetting(AppSettings.DatabaseUserNameVariableName, DatabaseUserName);

                if (!string.IsNullOrEmpty(DatabasePassword))
                    AppSettings.SetAppSetting(AppSettings.DatabasePasswordVariableName, DatabasePassword);
                
                logger.LogInformation("Application settings updated successfully");
            }
            catch (Exception exception)
            {
                logger.LogError(exception.Message);
            }
            return 0;
        }
    }
}