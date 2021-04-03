using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.Abstract
{
    public interface IModelRepositoryCreator<TEntity> : IRepositoryCreator<TEntity> where TEntity : IEntity { }
}
