using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IApplicationDatabaseStandaloneContextCreator
    {
        IApplicationDatabaseStandaloneContext Create();
    }

    public interface IApplicationDatabaseStandaloneContext : IDisposable
    {

    }

}
