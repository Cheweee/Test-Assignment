using Microsoft.EntityFrameworkCore;
using Test.Data.Models;

namespace Test.Data.DataAccessObjects.MSSql
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Instructor> Instructor { get; set; }

        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_connectionString);
        }
    }
}