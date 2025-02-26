using TrabajoPracticoP3.Data.Entities;
using System.Collections.Generic;

namespace TrabajoPracticoP3.Services.Interfaces
{
    public interface IClientServices
    {
        public List<Client> GetClients();
        public Client? GetClientById(int clientId);
        public int AddClient(Client client);
        public bool UpdateClient(Client client);
        public bool DeleteClient( int ClientId);
    }   
}
