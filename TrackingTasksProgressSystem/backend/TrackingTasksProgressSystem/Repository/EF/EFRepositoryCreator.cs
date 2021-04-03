using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;

namespace TrackingTasksProgressSystem.Repository.EF
{
    public class EFRepositoryCreator<TEntity> : EFRepositoryReader<TEntity>, IRepositoryCreator<TEntity> where TEntity : class, IEntity
    {
        public EFRepositoryCreator(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }


        public virtual async Task<int> AddAsync(TEntity entity)
        {
            if (entity is null) return 0;

            dbContext.Add(entity);

            return await SaveAsync();
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
    }
}
