using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IExecutionContextDependencyResolver
    {
        T Get<T>() where T : class;
        object Get(Type type);
    }
}
