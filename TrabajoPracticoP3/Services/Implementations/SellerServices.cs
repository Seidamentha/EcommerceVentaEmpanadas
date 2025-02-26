using System;
using System.Collections.Generic;
using System.Linq;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;
using TrabajoPracticoP3.Data.Enum;

namespace TrabajoPracticoP3.Services.Implementations
{
    public class SellerServices : ISellerServices
    {
        private readonly ECommerceContext _context;

        public SellerServices(ECommerceContext context)
        {
            _context = context;
        }

        // ✅ Agregar un producto
        public int AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product.IdProduct;
        }

        // ✅ Obtener lista de productos
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        // ✅ Obtener un producto por ID
        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.IdProduct == productId);
        }

        // ✅ Modificar un producto
        public bool UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.IdProduct == product.IdProduct);
            if (existingProduct == null)
                return false;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;

            _context.Products.Update(existingProduct);
            _context.SaveChanges();
            return true;
        }

        // ✅ Eliminar un producto (cambia su estado en lugar de eliminarlo físicamente)
        public bool DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.IdProduct == productId);
            if (product == null)
                return false;

            product.ProductState = ProductState.Disabled;
            _context.Products.Update(product);
            _context.SaveChanges();
            return true;
        }

        // ✅ Obtener clientes
        public List<User> GetClients()
        {
            return _context.Users.Where(c => c.UserType == UserType.Client).ToList();
        }

        // ✅ Crear un pedido
        public int CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.IdOrder;
        }

        // ✅ Modificar un pedido
        public bool UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.IdOrder == order.IdOrder);
            if (existingOrder == null)
                return false;

            existingOrder.ClientId = order.ClientId;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Quantity = order.Quantity;
            existingOrder.TotalPrice = order.TotalPrice;

            _context.Orders.Update(existingOrder);
            _context.SaveChanges();
            return true;
        }

        // ✅ Obtener vendedores
        public List<User> GetSellers()
        {
            return _context.Users.Where(u => u.UserType == UserType.Seller).ToList();
        }

        // ✅ Obtener un vendedor por ID
        public Seller GetSellerById(int id)
        {
            return _context.Sellers.FirstOrDefault(s => s.Id == id);
        }
    }
}

