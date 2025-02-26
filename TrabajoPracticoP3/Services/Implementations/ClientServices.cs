using TrabajoPracticoP3.Data.Entities;
using TrabajoPracticoP3.DBContext;
using TrabajoPracticoP3.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace TrabajoPracticoP3.Services.Implementations
{
    public class ClientServices : IClientServices
    {
        private readonly ECommerceContext _context;

        public ClientServices(ECommerceContext context)
        {
            _context = context;
        }

        public List<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Client? GetClientById(int clientId)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == clientId);
        }

        public int AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client.Id;
        }

        public bool UpdateClient(Client client)
        {
            var existingClient = _context.Clients.SingleOrDefault(c => c.Id == client.Id);
            if (existingClient == null) return false;

            existingClient.Name = client.Name;
            existingClient.SurName = client.SurName;
            existingClient.Email = client.Email;
            existingClient.UserName = client.UserName;
            existingClient.Password = client.Password;
            existingClient.PhoneNumber = client.PhoneNumber;
            existingClient.Address = client.Address;

            _context.SaveChanges();
            return true;

        }

       

        public bool DeleteClient(int clientId)
        {
            var clientToDelete = _context.Clients.SingleOrDefault(c => c.Id == clientId);
            if (clientToDelete == null) return false;

            _context.Clients.Remove(clientToDelete);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int ClientId)
        {
            throw new NotImplementedException();
        }
    }
}
