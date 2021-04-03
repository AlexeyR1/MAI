using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.Abstract
{
    public interface IModelRepositoryDeleter<TEntity> : IRepositoryDeleter<TEntity> where TEntity : IEntity { }
}
