using Tasks = System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.DTO.Abstract;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Controllers.Abstract
{
    public abstract class DeleterController<TEntity, TDto> : RootController<TEntity, TDto, IRepositoryDeleter<TEntity>>
        where TEntity : IEntity
        where TDto : IDto
    {
        public DeleterController(IRepositoryDeleter<TEntity> repository, IDtoTranformerService<TEntity, TDto> dtoTransformer) : base(repository, dtoTransformer) { }


        [HttpDelete("{id}")]
        public async Tasks.Task<IActionResult> Delete(int id)
        {
            if (await repository.DeleteAsync(id) == 0) return BadRequest();

            return NoContent();
        }
    }
}
