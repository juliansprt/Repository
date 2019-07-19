using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Core.EntityFramework
{
    public class Repository<tEntity> : IRepository<tEntity>
        where tEntity : class, new()
    {

        public IApplicationDataContext ApplicationDataContext { get; set; }

        protected DbSet<tEntity> DbSet => this.ApplicationDataContext.Set<tEntity>().ToDbSet<tEntity>();

        public Repository(IApplicationDataContext applicationDatabaseContext)
        {
            this.ApplicationDataContext = applicationDatabaseContext;
        }

        public tEntity this[int id] => this.Get(id);

        public Type ElementType => typeof(tEntity);

        public Expression Expression => this.GetQueryable().Expression;

        public IQueryProvider Provider => this.GetQueryable().Provider;

        public void Add(tEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<tEntity> entity)
        {
            this.DbSet.AddRange(entity);
        }

        public int Count()
        {
            return this.DbSet.Count();
        }

        public tEntity Get(int id)
        {
            return this.DbSet.Find(id);
        }

        public List<tEntity> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IEnumerator<tEntity> GetEnumerator()
        {
            return this.GetQueryable().GetEnumerator();
        }

        public void Remove(tEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            this.DbSet.Remove(this.Get(id));
        }

        public void Start()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected IQueryable<tEntity> GetQueryable()
        {
            return this.DbSet;
        }
    }
}
