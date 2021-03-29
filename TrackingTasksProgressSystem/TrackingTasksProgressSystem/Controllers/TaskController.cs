using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Services.DTOTransformers;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : DeleterController<Task, TaskDTO>
    {
        public TaskController(TrackingTasksProgressDbContext dbContext) : base(new EFTaskRepository(dbContext),
                                                                               new TaskDTOTransformerService(dbContext))
        { }
    }
}
