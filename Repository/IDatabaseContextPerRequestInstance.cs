using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IDatabaseContextPerRequestInstance
    {
        IApplicationDatabaseContext Get(string id);

        void Set(string id, IApplicationDatabaseContext context);

        IApplicationDatabaseContext GetCurrentContext();
    }
}
