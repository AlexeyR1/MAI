using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers.Abstract
{
    public interface IReadOnlyDtoTranformer<TEntity, TDto>
        where TEntity : IEntity
        where TDto : IDto
    {
        TDto ToDto(TEntity entity);
    }
}
