using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IApplicationDatabaseContext
    {
        object Set<T>() where T : class;
        void Save();
        void BeginTransaction();
        void RollBack();
        void Commit();
        object Entry<T>(T enity) where T : class;
        void DisableValidation();
        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);
        void ExecuteNonQuery(string query, params object[] parameters);

    }
}
