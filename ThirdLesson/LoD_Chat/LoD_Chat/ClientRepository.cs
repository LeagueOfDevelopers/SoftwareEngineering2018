using System;
using System.Collections.Generic;

namespace LoD_Chat
{
    public class ClientRepository
    {
        private readonly List<IClient> _clients;

        public IClient[] Clients => _clients.ToArray();

        public IClient GetClient(Guid clientId)
        {
            return TryGetClient(clientId) ?? throw new InvalidOperationException(
                $"Chat with id {clientId} not found");
        }

        public void AddClient(IClient client)
        {
            IClient existantClient = TryGetClient(client.Id);

            if (existantClient != null)
            {
                _clients.Remove(existantClient);
            }

            _clients.Add(client);

        }

        public bool DoesClientExist(Guid clientId)
        {
            return TryGetClient(clientId) != null;
        }


        private IClient TryGetClient(Guid clientId)
        {
            foreach (var client in _clients)
            {
                if (client.Id == clientId)
                {
                    return client;
                }
            }

            return null;
        }
    }
}
