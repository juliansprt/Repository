using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Business.Models;

namespace WebApplication.Business
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();

        void Create(Client client);
    }
}
