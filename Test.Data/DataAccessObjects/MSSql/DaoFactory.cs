using Microsoft.Extensions.Logging;
using Test.Data.Interfaces;

namespace Test.Data.DataAccessObjects.MSSql
{
    public class DaoFactory : IDaoFactory
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public DaoFactory(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public IInstructorDao InstructorDao => new InstructorDao(_connectionString, _logger);
    }
}