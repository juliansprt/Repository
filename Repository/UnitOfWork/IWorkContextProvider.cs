using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UnitOfWork
{
    public interface IWorkContextProvider
    {
        WorkContext CurrentContext { get; }
    }
}
