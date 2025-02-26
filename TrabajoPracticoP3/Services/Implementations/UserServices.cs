using System;
using System.Linq;
using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;
using TrabajoPracticoP3.Data.Enum;



namespace TrabajoPracticoP3.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly ECommerceContext _context;

        public UserServices(ECommerceContext context)
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
            User userToDelete = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (userToDelete != null)
            {
                userToDelete.UserState = StateUser.Disabled;
                _context.Update(userToDelete);
                _context.SaveChanges();
            }
        }

        public User GetByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public int UpdateUser(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;

                _context.Update(existingUser);
                _context.SaveChanges();

                return existingUser.Id;
            }

            return 0; // Indicar que no se encontró el usuario
        }

        public BaseResponse ValidarUsuario(string email, string password)
        {
            BaseResponse response = new BaseResponse();
            User userForLogin = _context.Users.SingleOrDefault(u => u.Email == email);

            if (userForLogin != null)
            {
                if (userForLogin.Password == password)
                {
                    response.Result = true;
                    response.Message = "Login successful";
                }
                else
                {
                    response.Result = false;
                    response.Message = "Incorrect password";
                }
            }
            else
            {
                response.Result = false;
                response.Message = "User not found";
            }

            return response;
        }
    }
}
