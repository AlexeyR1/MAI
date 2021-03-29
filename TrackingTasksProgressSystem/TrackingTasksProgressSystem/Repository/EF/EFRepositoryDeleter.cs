using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.EFCore;

namespace TrackingTasksProgressSystem.Repository.EF
{
    public class EFRepositoryDeleter<TEntity> : EFRepositoryBase<TEntity>, IRepositoryDeleter<TEntity> where TEntity : class, IEntity
    {
        public EFRepositoryDeleter(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }


        public async Task<int> DeleteAsync(int id)
        {
            var itemToRemove = GetById(id);
            if (itemToRemove is not null)
            {
                dbContext.Remove(itemToRemove);
                return await SaveAsync();
            }

            return 0;
        }
    }
}
