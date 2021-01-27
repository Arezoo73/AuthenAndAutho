using Shop.Inferastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Inferastructure.IRepository
{
    public interface IProductRepository
    {
        List<Product> GetAllProduct();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdatedProduct(Product product);
        void RemoveProduct(int id);
    }
}
