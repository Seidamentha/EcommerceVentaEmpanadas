using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.Data.Models;

namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface IUserServices
    {
        public int CreateUser(User user);

        public int UpdateUser(User user);

        public void DeleteUser(int userId);

        public User GetByEmail(string email);

        public BaseResponse ValidarUsuario(string username, string password);
    }
}

