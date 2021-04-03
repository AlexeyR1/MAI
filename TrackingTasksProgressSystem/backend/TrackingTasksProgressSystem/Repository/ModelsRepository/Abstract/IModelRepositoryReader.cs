using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.Abstract
{
    public interface IModelRepositoryReader<TEntity> : IRepositoryReader<TEntity> where TEntity : IEntity { }
}
