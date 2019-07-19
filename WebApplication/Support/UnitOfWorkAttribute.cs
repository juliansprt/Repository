using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Support
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        private const string ExecutionKey = "UnitOfWorkAttribute.UnitOfWorkExecution";

        public UnitOfWorkAttribute()
        {
            this.UseTransaction = false;
        }

        public bool UseTransaction { get; set; }


        private static string GetUowName(ControllerActionDescriptor actionDescriptor)
        {
            return $"{actionDescriptor.ControllerName}.{actionDescriptor.ActionName}";
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var workProvider = (IWorkContextProvider)context.HttpContext.RequestServices.GetService(typeof(IWorkContextProvider));
            var executionContext = (IExecutionContext)context.HttpContext.RequestServices.GetService(typeof(IExecutionContext));
            var applicationDatabaseContext = (IDatabaseContextPerRequestInstance)context.HttpContext.RequestServices.GetService(typeof(IDatabaseContextPerRequestInstance));
            

            var unitOfWork =
                workProvider.CurrentContext.BeginUnitOfWork(GetUowName((ControllerActionDescriptor)context.ActionDescriptor),
                    applicationDatabaseContext.Get(context.HttpContext.Connection.Id), this.UseTransaction);
            unitOfWork.OnEnd(() => executionContext.RemoveObject(ExecutionKey));

            executionContext.SetObject(ExecutionKey, unitOfWork);

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var executionContext = (IExecutionContext)context.HttpContext.RequestServices.GetService(typeof(IExecutionContext));

            base.OnActionExecuted(context);
            var execution = executionContext.GetObject<UnitOfWorkExecution>(ExecutionKey);

            if (context.Exception != null)
                execution?.HandleException(execution.Exception);

            execution?.End();
        }
    }
}
