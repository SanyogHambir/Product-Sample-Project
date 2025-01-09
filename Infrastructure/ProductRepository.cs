using Dapper;
using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.Models;
using System.Data;

namespace Product.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;


        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Products product)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var query = "INSERT INTO Products (ProductName, Created) VALUES (@ProductName, @Created)";
                connection.Execute(query, product);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var query = "DELETE FROM Products WHERE SN = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public IEnumerable<Products> GetAll()
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                return connection.Query<Products>("SELECT * FROM Products");
            }
        }

        public Products GetById(int id)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                return connection.QuerySingleOrDefault<Products>("SELECT * FROM Products WHERE SN = @Id", new { Id = id });
            }
        }

        public void Update(Products product)
        {
            using (IDbConnection connection = _context.CreateConnection())
            {
                var query = "UPDATE Products SET ProductName = @ProductName, Created = @Created WHERE SN = @SN";
                connection.Execute(query, product);
            }
        }
    }
}
