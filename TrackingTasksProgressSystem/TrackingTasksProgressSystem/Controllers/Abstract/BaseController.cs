using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{

    public abstract class BaseController<TEntity, TDto> : RootController<TEntity, TDto, IRepositoryBase<TEntity>>
        where TEntity : IEntity
        where TDto : IDto
    {
        public BaseController(IRepositoryBase<TEntity> repository, IDtoTranformer<TEntity, TDto> dtoTransformer) : base(repository, dtoTransformer) { }
    }
}
