using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Business.Models;

namespace WebApplication.Business
{

    public abstract class DBModelBaseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TestBase;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework");
        }
    }


    [Export(InstanceType.InstancePerRequest)]
    public partial class DBModel : DBModelBaseContext, IApplicationDatabaseContext
    {
        public virtual DbSet<Client> Clients { get; set; }

        public void BeginTransaction()
        {
            this.Database.BeginTransaction();
        }

        public void Commit()
        {
            this.Database.CommitTransaction();
        }

        public void DisableValidation()
        {
            this.DisableValidation();
        }

        public void ExecuteNonQuery(string query, params object[] parameters)
        {
        }

        public void RollBack()
        {
            this.Database.RollbackTransaction();
        }

        public void Save()
        {
            var entities = this.ChangeTracker.Entries();

            this.SaveChanges();
        }

        public IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return null;
        }

        object IApplicationDatabaseContext.Entry<T>(T enity)
        {
            return this.Entry<T>(enity);
        }

        object IApplicationDatabaseContext.Set<T>()
        {
            return this.Set<T>();
        }
    }


}
