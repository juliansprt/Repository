using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public interface IRepository<tEntity> : IQueryable<tEntity>, IExportInitializable
        where tEntity : class
    {
        tEntity this[int id] { get; }

        List<tEntity> GetAll();

        tEntity Get(int id);

        void Add(tEntity entity);
        void AddRange(IEnumerable<tEntity> entity);

        void Remove(tEntity entity);

        void Remove(int id);

        int Count();
    }
}
