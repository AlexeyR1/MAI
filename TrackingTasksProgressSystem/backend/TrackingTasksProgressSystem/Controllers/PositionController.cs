using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Services.DTOTransformers;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ReaderController<Position, PositionDTO>
    {
        public PositionController(TrackingTasksProgressDbContext dbContext) : base(new EFPositionRepository(dbContext),
                                                                                   new PositionDTOTransformer())
        { }
    }
}
