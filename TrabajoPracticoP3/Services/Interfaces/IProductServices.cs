
using TrabajoPracticoP3.Data.Entities;

namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface IProductServices
    {
        public int CreateProduct(Product product);
        public int UpdateProduct(Product product);
        public void DeleteProduct(int productId);
        public Product GetProductById(int id);
        public List<Product> GetAllProducts();

    }
}