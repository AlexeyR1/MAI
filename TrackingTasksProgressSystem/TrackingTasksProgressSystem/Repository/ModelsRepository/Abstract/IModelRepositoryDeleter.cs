using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.Abstract
{
    public interface IModelRepositoryDeleter<TEntity> : IRepositoryDeleter<TEntity> where TEntity : IEntity { }
}
