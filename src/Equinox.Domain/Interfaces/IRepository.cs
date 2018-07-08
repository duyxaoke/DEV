using System;
using System.Linq;
using System.Linq.Expressions;

namespace Equinox.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);

        TEntity GetById(Guid id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        void Update(TEntity obj);

        void Remove(Guid id);

        int SaveChanges();
    }
}