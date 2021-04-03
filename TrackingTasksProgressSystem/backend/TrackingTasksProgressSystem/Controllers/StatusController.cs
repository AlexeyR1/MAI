using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Services.DTOTransformers;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : ReaderController<Status, StatusDTO>
    {
        public StatusController(TrackingTasksProgressDbContext dbContext) : base(new EFRepositoryReader<Status>(dbContext),
                                                                                 new StatusDTOTransformer()) { }
    }
}
