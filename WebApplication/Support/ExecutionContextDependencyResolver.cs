using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Support
{

    [Export(InstanceType.SingleInstance)]
    public class ExecutionContextDependencyResolver : IExecutionContextDependencyResolver
    {
        private IServiceProvider ServiceProvider;
        public ExecutionContextDependencyResolver(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }
        public T Get<T>() where T : class
        {
            return (T)this.ServiceProvider.GetService(typeof(T));
        }

        public object Get(Type type)
        {
            return this.ServiceProvider.GetService(type);
        }
    }
}
