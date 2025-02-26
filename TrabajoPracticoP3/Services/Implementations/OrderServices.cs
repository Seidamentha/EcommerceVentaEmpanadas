using Microsoft.EntityFrameworkCore;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;

namespace TrabajoPracticoP3.Services.Implementations
{
    public class OrderServices : IOrderServices
    {
        private readonly ECommerceContext _context;

        public OrderServices(ECommerceContext context)
        {
            _context = context;
        }

        public Order GetOrder(int id)
        {
            var order = _context.Orders
                .Include(o => o.Client)
                .Include(o => o.SaleOrderLines)
                .SingleOrDefault(o => o.Id == id);

            return order ?? throw new Exception("Order not found");
        }


        public int CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }
        public int AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }
        public bool UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder == null)
            {
                return false; // Pedido no encontrado
            }

            // Actualizar los campos necesarios
            existingOrder.ClientId = order.ClientId;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Quantity = order.Quantity;
            existingOrder.TotalPrice = order.TotalPrice;

            _context.Orders.Update(existingOrder);
            _context.SaveChanges();

            return true; // Actualización exitosa
        }
        public Order GetLatestOrderForClient(int clientId)
        {
            return _context.Orders
                .Where(o => o.ClientId == clientId)
                .OrderByDescending(o => o.CreationDate)
                .FirstOrDefault();
        }


    }
}
