
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;


namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface IProductServices
    {
        public int AddProduct(Product product);
        
        public int UpdateProduct(Product product);
        public bool DeleteProduct(int productId);
        public Product? GetProductById(int id);
        public List<Product> GetAllProducts();
        //object GetAllProducts();
    }
}