using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRent
{
    public class ClientRepository : IRepository<Client>
    {
        private List<Client> Clients { get; set; }

        public ClientRepository()
        {
            Clients = new List<Client>();
        }

        public ClientRepository(List<Client> clients)
        {
            Clients = clients;
        }

        public void Create(Client client)
        {
            Clients.Add(client);
        }

        public void Delete(Client client)
        {
            Clients.Remove(client);
        }

        public Client GetItem(string name)
        {
            return Clients.Find(x => x.Name == name);
        }

        public Client GetItem(int position)
        {
            return Clients.ToArray()[position];
        }

        public IEnumerable<Client> GetList()
        {
            return Clients;
        }

        public int GetSize()
        {
            return Clients.Count;
        }

        public void Update(Client client)
        {
            Clients.RemoveAll(x => x.Name == client.Name);
            Clients.Add(client);
        }
    }
}
