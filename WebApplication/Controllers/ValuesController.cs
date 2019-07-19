using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Business;
using WebApplication.Business.Models;
using WebApplication.Support;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [IoCScope]
    [UnitOfWork]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IClientService ClientService;
        public ValuesController(
            IClientService clientService)
        {
            this.ClientService = clientService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Client>> Get()
        {
            return this.ClientService.GetAll().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [UnitOfWork(UseTransaction = true)]
        public ActionResult<string> Get(int id)
        {
            this.ClientService.Create(new Client()
            {
                LastName = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString()
            });
            return "OK";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
