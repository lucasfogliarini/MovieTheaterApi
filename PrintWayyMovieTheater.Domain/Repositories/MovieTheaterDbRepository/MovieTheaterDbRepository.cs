using Microsoft.EntityFrameworkCore;
using PrintWayyMovieTheater.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PrintWayyMovieTheater.Domain.Repositories
{
    internal class MovieTheaterDbRepository : IMovieTheaterDbRepository
    {
        readonly DbContext _dbContext;
        public MovieTheaterDbRepository(MovieTheaterDbContext movieTheaterDbContext)
        {
            _dbContext = movieTheaterDbContext;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Add(entity);
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            _dbContext.AddRange(entities);
        }
        public int Commit()
        {
            var changes = _dbContext.SaveChanges();
            DetachEntries();
            return changes;
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Update(entity);
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Remove(entity);
        }
        public void DetachEntries()
        {
            var entities = _dbContext.ChangeTracker.Entries();
            foreach (var e in entities)
            {
                e.State = EntityState.Detached;
            }
        }
    }
}
