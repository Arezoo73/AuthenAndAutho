using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Inferastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ICommandText _commandText;
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration, ICommandText commandText)
        {
            _commandText = commandText;
            _connectionString = configuration.GetConnectionString("Dapper");
        }
        public List<Product> GetAllProduct()
        {
            var query = ExecuteCommand(_connectionString, conn => conn.Query<Product>(_commandText.GetProducts))
                .ToList();
            return query;
        }
        public Product GetProductById(int id)
        {
            var query = ExecuteCommand<Product>(_connectionString, conn => conn.Query<Product>(_commandText.GetProductById, new { @Id = id }).SingleOrDefault());

            return query;
        }
        public void AddProduct(Product product)
        {
            ExecuteCommand(_connectionString,
                conn => conn.Query<Product>(_commandText.AddProduct,
                new { Name = product.Name, Quantity=product.Quantity, Price = product.Price, CreateDate = product.CreateDate }));
        }
        public void UpdatedProduct(Product product)
        {
            ExecuteCommand(_connectionString,
                conn => conn.Query<Product>(_commandText.UpdateProduct,
                    new { Name = product.Name, Quantity = product.Quantity, Price = product.Price, CreateDate = product.CreateDate,Id=product.Id}));
        }
        public void RemoveProduct(int id)
        {
            ExecuteCommand(_connectionString,
                conn =>
                {
                    var query = conn.Query<Product>(_commandText.RemoveProduct,
                        new { Id = id });
                });
        }
        private void ExecuteCommand(string connection, Action<SqlConnection> task)
        {
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                task(conn);
            }
        }
        private T ExecuteCommand<T>(string connection, Func<SqlConnection, T> task)
        {
            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                return task(conn);
            }
        }
    }
}
