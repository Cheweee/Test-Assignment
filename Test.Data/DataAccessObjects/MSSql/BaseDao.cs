using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Test.Data.DataAccessObjects.MSSql
{
    public abstract class MSSqlBaseDao<TEntity> where TEntity : class
    {
        protected readonly string _connectionString;
        protected readonly ILogger _logger;

        public MSSqlBaseDao(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        protected async Task CreateEntity(TEntity entity)
        {
            try
            {
                using (var _context = new DatabaseContext(_connectionString))
                {
                    await _context.Set<TEntity>().AddAsync(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        protected async Task DeleteEntity(int id)
        {
            try
            {
                using (var _context = new DatabaseContext(_connectionString))
                {
                    var model = await _context.Set<TEntity>().FindAsync(id);
                    _context.Remove(model);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        protected async Task<IEnumerable<TEntity>> GetEntities(Func<TEntity, bool> predicate)
        {
            try
            {
                using (var _context = new DatabaseContext(_connectionString))
                {
                    var result = await _context.Set<TEntity>().ToListAsync();
                    return result.Where(predicate);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }

        protected async Task UpdateEntity(TEntity model)
        {
            try
            {
                using (var _context = new DatabaseContext(_connectionString))
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw exception;
            }
        }
    }
}