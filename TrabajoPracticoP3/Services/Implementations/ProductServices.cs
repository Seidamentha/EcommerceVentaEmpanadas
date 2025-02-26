using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public int AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product.Id;
        }

        public bool DeleteProduct(int productId)
        {
            var productToDelete = _context.Products.SingleOrDefault(p => p.Id == productId);
            if (productToDelete == null) return false;
            
                _context.Remove(productToDelete);
                int changes = _context.SaveChanges();
            
            return changes > 0;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                throw new Exception($"No se encontró el producto con ID {id}");
            }

            return product;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }


        public int UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return _context.SaveChanges(); // Devuelve el número de filas afectadas
        }

    }
}
