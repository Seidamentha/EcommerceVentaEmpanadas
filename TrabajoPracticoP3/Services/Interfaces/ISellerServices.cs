using System.Collections.Generic;
using TrabajoPracticoP3.Data.Entities;

namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface ISellerServices
    {
        int AddProduct(Product product);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool UpdateProduct(Product product);
        bool DeleteProduct(int productId);

        List<User> GetClients();
        int CreateOrder(Order order);
        bool UpdateOrder(Order order);

        List<User> GetSellers();
        Seller GetSellerById(int id);
    }
}


