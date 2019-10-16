using Microsoft.Extensions.Logging;
using Test.Data.Enumerations;
using Test.Data.Interfaces;

namespace Test.Data
{
    public class DaoFactories
    {
        public static IDaoFactory GetFactory(DataProvider provider, string connectionString, ILogger logger)
        {
            switch (provider)
            {
                case DataProvider.MSSql:
                    return new DataAccessObjects.MSSql.DaoFactory(connectionString, logger);
                default:
                    return new DataAccessObjects.MSSql.DaoFactory(connectionString, logger);
            }
        }
    }
}