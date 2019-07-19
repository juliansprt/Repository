using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.UnitOfWork
{
    public enum UnitOfWorkStatus
    {
        NotStarted,
        Running,
        Successfull,
        Failed,
        Canceled
    }
}
