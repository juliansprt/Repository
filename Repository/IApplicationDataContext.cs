using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IApplicationDataContext
    {
        object Set<T>() where T : class;

        void Save();

        void BeginTransaction();

        void RollBack();

        void Commit();

        void DisableValidation();

        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);

        void ExecuteNonQuery(string query, params object[] parameters);

        IApplicationDatabaseContext GetApplicationDatabaseContext();

    }
}
