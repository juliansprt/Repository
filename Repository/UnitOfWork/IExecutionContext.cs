using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UnitOfWork
{
    public interface IExecutionContext
    {
        T GetObject<T>(string key);
        void SetObject(string key, object val);
        void RemoveObject(string key);
    }
}
