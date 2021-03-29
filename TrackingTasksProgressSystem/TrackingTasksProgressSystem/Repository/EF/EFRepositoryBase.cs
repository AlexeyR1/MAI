using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.EFCore;
using Microsoft.EntityFrameworkCore;

namespace TrackingTasksProgressSystem.Repository.EF
{
    public class EFRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        private protected TrackingTasksProgressDbContext dbContext;


        public EFRepositoryBase(TrackingTasksProgressDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public virtual async Task<int> AddAsync(TEntity entity)
        {
            if (entity is null) return 0;

            dbContext.Add(entity);

            return await SaveAsync();
        }


        public TEntity? GetById(int id)
        {
            return GetAll().SingleOrDefault(entity => entity.Id == id);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsEnumerable();
        }


        // Не требует передачи id в json
        public virtual async Task<int> UpdateAsync(int id, TEntity entity)
        {
            if (entity is null) return 0;

            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry(entity).Property(item => item.Id).CurrentValue = id;
            dbContext.Set<TEntity>().Update(entity);

            return await SaveAsync();
        }


        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                    dbContext = null;
                }
            }
        }
    }
}
