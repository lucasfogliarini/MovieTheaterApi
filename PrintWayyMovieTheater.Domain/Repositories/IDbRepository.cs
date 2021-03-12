using PrintWayyMovieTheater.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PrintWayyMovieTheater.Domain.Repositories
{
    public interface IDbRepository
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        int Commit();
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void DetachEntries();
    }
}
