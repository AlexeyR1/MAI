using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{
    public abstract class ReaderController<TEntity, TDto> : RootController<TEntity, TDto, IRepositoryReader<TEntity>, IReadOnlyDtoTranformer<TEntity,TDto>>

        where TEntity : IEntity
        where TDto : IDto
    {
        public ReaderController(IRepositoryReader<TEntity> repository, IReadOnlyDtoTranformer<TEntity, TDto> dtoTransformer) : base(repository, dtoTransformer) { }
    }
}
