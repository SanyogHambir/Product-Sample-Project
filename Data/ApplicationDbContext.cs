using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Product.Models;
using System.Data;

namespace Product.Data
{
    public class ApplicationDbContext:DbContext
    {
        private readonly string _connectionString;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            _connectionString = options.FindExtension<SqlServerOptionsExtension>()?.ConnectionString;

        }
        public DbSet<Products> Products { get; set; }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open(); 
            return connection;
        }
    }
}
