using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.Abstract
{
    public interface IRepositoryReader<TEntity> : IDisposable where TEntity : IEntity
    {
        TEntity? GetById(int id);
        IEnumerable<TEntity> GetAll();
        Task<int> SaveAsync();
    }
}
