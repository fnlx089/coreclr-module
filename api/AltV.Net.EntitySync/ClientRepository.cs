using System.Collections.Generic;

namespace AltV.Net.EntitySync
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDictionary<string, IClient> clients = new Dictionary<string, IClient>();

        private readonly ClientThreadRepository[] clientThreadRepositories;

        public ClientRepository(ClientThreadRepository[] clientThreadRepositories)
        {
            this.clientThreadRepositories = clientThreadRepositories;
        }

        public void Add(IClient client)
        {
            lock (clients)
            {
                foreach (var clientThreadRepository in clientThreadRepositories)
                {
                    clientThreadRepository.Add(client);
                }

                clients[client.Token] = client;
            }
        }

        public IClient Remove(IClient client)
        {
            return Remove(client.Token);
        }

        public IClient Remove(string token)
        {
            lock (clients)
            {
                if (clients.Remove(token, out var client))
                {
                    foreach (var clientThreadRepository in clientThreadRepositories)
                    {
                        clientThreadRepository.Remove(client);
                    }

                    return client;
                }
            }

            return null;
        }

        public bool TryGet(string token, out IClient client)
        {
            lock (clients)
            {
                return clients.TryGetValue(token, out client);
            }
        }
    }
}