using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core.EntityFramework
{

    [Export(InstanceType.SingleInstance)]
    public class ApplicationDataContext : IApplicationDataContext
    {
        public IDatabaseContextPerRequestInstance ExecutionContextDependencyResolver { get; set; }

        protected IApplicationDatabaseContext ApplicationDatabaseContext => this.ExecutionContextDependencyResolver
            .GetCurrentContext();

        public ApplicationDataContext(IDatabaseContextPerRequestInstance executionContextDependencyResolver)
        {
            this.ExecutionContextDependencyResolver = executionContextDependencyResolver;
        }
        public void BeginTransaction()
        {
            this.ApplicationDatabaseContext.BeginTransaction();
        }

        public void Commit()
        {
            this.ApplicationDatabaseContext.Commit();
        }

        public void DisableValidation()
        {
            this.ApplicationDatabaseContext.DisableValidation();
        }

        public void ExecuteNonQuery(string query, params object[] parameters)
        {
            this.ApplicationDatabaseContext.ExecuteNonQuery(query, parameters);
        }

        public void RollBack()
        {
            this.ApplicationDatabaseContext.RollBack();
        }

        public void Save()
        {
            this.ApplicationDatabaseContext.Save();
        }

        public IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return this.ApplicationDatabaseContext.SqlQuery<T>(query, parameters);
        }

        public IApplicationDatabaseContext GetApplicationDatabaseContext()
        {
            return ApplicationDatabaseContext;
        }

        object IApplicationDataContext.Set<T>()
        {
            return this.ApplicationDatabaseContext.Set<T>().ToDbSet<T>();
        }
    }
}
