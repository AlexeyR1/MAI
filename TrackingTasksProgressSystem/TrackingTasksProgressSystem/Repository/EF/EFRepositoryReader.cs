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
    public class EFRepositoryReader<TEntity> : IRepositoryReader<TEntity> where TEntity : class, IEntity
    {
        private protected TrackingTasksProgressDbContext dbContext;


        public EFRepositoryReader(TrackingTasksProgressDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public TEntity? GetById(int id)
        {
            return GetAll().SingleOrDefault(entity => entity.Id == id);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().AsEnumerable();
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
