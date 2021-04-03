using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.DTO.ReadOnly;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Services.DTOTransformers;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    public class ShortTaskController : ReaderController<Models.Task, ShortTaskDTO>
    {
        public ShortTaskController(TrackingTasksProgressDbContext dbContext) : base(new EFTaskRepository(dbContext),
                                                                                    new ShortTaskDtoTransformer())
        { }
    }
}
