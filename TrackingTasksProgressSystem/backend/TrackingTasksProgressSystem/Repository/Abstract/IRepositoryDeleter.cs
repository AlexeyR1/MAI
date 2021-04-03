using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.Abstract
{
    public interface IRepositoryDeleter<TEntity> : IRepositoryCreator<TEntity> where TEntity : IEntity
    {
        Task<int> DeleteAsync(int id);
    }
}
