using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Test.Data.Interfaces;
using Test.Data.Models;

namespace Test.Data.DataAccessObjects.MSSql
{
    public class InstructorDao : MSSqlBaseDao<Instructor>, IInstructorDao
    {
        public InstructorDao(string connectionString, ILogger logger) : base(connectionString, logger) { }

        public async Task<Instructor> Create(Instructor model)
        {
            _logger.LogInformation("Trying to execute sql insert instructor query");
            await CreateEntity(model);
            _logger.LogInformation("Sql insert instructor query successfully executed");

            return model;
        }

        public async Task Delete(int id)
        {
            _logger.LogInformation("Trying to execute sql delete instructor query");
            await DeleteEntity(id);
            _logger.LogInformation("Sql delete instructor query successfully executed");
        }

        public async Task<IEnumerable<Instructor>> Get(InstructorGetOptions options)
        {
            _logger.LogInformation("Trying to execute sql get instructor query");

            var result = await GetEntities(o =>
                (options.Id.HasValue ? o.Id == options.Id.Value : true)
                && (string.IsNullOrEmpty(options.Search) ? true
                    : o.FirstName.ToLower().Contains(options.Search.ToLower()))
            );

            _logger.LogInformation("Sql get instructor query successfully executed");
            return result;
        }

        public async Task<Instructor> Update(Instructor model)
        {
            _logger.LogInformation("Trying to execute sql update instructor query");
            await UpdateEntity(model);
            _logger.LogInformation("Sql update instructor query successfully executed");

            return model;
        }
    }
}