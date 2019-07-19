using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Support
{
    public class IoCScopeAttribute : ActionFilterAttribute
    {
        public const string dependencyScopeKey = "WebAppication.DependencyResolver.key";

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var databaseInstance = (IApplicationDatabaseContext)context.HttpContext.RequestServices.GetService(typeof(IApplicationDatabaseContext));
            var executionContext = (IDatabaseContextPerRequestInstance)context.HttpContext.RequestServices.GetService(typeof(IDatabaseContextPerRequestInstance));
            executionContext.Set(context.HttpContext.Connection.Id, databaseInstance);
        }
    }
}
