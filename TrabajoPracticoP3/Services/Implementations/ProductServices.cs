using System.Collections.Generic;
using System.Linq;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;

namespace TrabajoPracticoP3.Services.Implementations
{
    public class ProductService : IProductServices
    {
        private readonly ECommerceContext _context;

        public ProductService(ECommerceContext context)
        {
            _context = context;
        }

        public int CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product.IdProduct;
        }

        public void DeleteProduct(int productId)
        {
            var productToDelete = _context.Products.SingleOrDefault(p => p.IdProduct == productId);
            if (productToDelete != null)
            {
                _context.Remove(productToDelete);
                _context.SaveChanges();
            }
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.IdProduct == id);
        }

        public int UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.SingleOrDefault(p => p.IdProduct == product.IdProduct);

            if (existingProduct == null)
            {
                return 0;
            }

            existingProduct.NameProduct = product.NameProduct;
            existingProduct.Price = product.Price;

            _context.Update(existingProduct);
            _context.SaveChanges();
            return existingProduct.IdProduct;
        }
    }
}
