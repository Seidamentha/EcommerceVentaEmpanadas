using TrabajoPracticoP3.Data.Entities;

namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface IAdminServices
    {
        int CreateUser(User user);
        void DeleteUser(int userId);
        List<User> GetAdmins();
        List<User> GetClients();
        List<Product> GetProducts();
        Product GetProductById(int productId);

    }
}
