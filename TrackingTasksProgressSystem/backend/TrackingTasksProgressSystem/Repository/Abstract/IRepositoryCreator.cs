using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.Abstract
{
    public interface IRepositoryCreator<TEntity> :  IRepositoryReader<TEntity> where TEntity : IEntity
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(int id, TEntity entity);
    }
}
