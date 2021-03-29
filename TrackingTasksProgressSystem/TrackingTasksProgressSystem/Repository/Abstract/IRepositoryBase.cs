using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.Abstract
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : IEntity
    {
        Task<int> AddAsync(TEntity entity);
        TEntity? GetById(int id);
        IEnumerable<TEntity> GetAll();
        Task<int> UpdateAsync(int id, TEntity entity);
        Task<int> SaveAsync();
    }
}
