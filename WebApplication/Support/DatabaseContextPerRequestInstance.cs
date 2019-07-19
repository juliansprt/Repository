using Microsoft.AspNetCore.Http;
using Repository;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Support
{
    [Export]
    public class DatabaseContextPerRequestInstance : IDatabaseContextPerRequestInstance
    {
        private IExecutionContext ExecutionContext;
        private IHttpContextAccessor HttpContextAccessor;

        public DatabaseContextPerRequestInstance(
            IExecutionContext executionContext,
            IHttpContextAccessor httpContextAccessor)
        {
            this.ExecutionContext = executionContext;
            this.HttpContextAccessor = httpContextAccessor;
        }
        public IApplicationDatabaseContext Get(string id)
        {
            return this.ExecutionContext.GetObject<IApplicationDatabaseContext>(id);
        }

        public IApplicationDatabaseContext GetCurrentContext()
        {
            return this.Get(this.HttpContextAccessor.HttpContext.Connection.Id);
        }

        public void Set(string id, IApplicationDatabaseContext context)
        {
            this.ExecutionContext.SetObject(id, context);
        }
    }
}
