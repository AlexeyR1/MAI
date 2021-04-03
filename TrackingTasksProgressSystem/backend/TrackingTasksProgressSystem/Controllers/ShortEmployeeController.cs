using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.DTO.ReadOnly;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Services.DTOTransformers;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    public class ShortEmployeeController : ReaderController<Employee, ShortEmployeeDTO>
    {
        public ShortEmployeeController(TrackingTasksProgressDbContext dbContext)
            : base(new EFEmployeeRepository(dbContext), new ShortEmployeeDTOTransformer())
        { }
    }
}
