using System.Collections.Generic;
using System.Linq;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;
using TrabajoPracticoP3.Data.Enum;


namespace TrabajoPracticoP3.Services.Implementations
{
    public class AdminServices : IAdminServices
    {
        private readonly ECommerceContext _context;

        public AdminServices(ECommerceContext context)
        {
            _context = context;
        }

        public int CreateUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public void DeleteUser(int userId)
        {
            User? userDelete = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userDelete != null)
            {
                userDelete.UserState = Data.Entities.StateUser.Disabled;
                
                _context.Update(userDelete);
                _context.SaveChanges();
            }
        }

        public List<User> GetAdmins()
        {
            return _context.Users.Where(u => u.UserType == UserType.Admin).ToList();
        }

        public List<User> GetClients()
        {
            return _context.Users.Where(c => c.UserType == UserType.Client).ToList();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.IdProduct == productId);
        }
    }
}
