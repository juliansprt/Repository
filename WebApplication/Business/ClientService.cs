using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Business.Models;

namespace WebApplication.Business
{
    [Export(InstanceType.SingleInstance)]
    public class ClientService : IClientService
    {
        private IRepository<Client> Clients;

        public ClientService(
            IRepository<Client> clients)
        {
            this.Clients = clients;
        }

        public void Create(Client client)
        {
            this.Clients.Add(client);
        }

        public IEnumerable<Client> GetAll()
        {
            return this.Clients;
        }
    }
}
