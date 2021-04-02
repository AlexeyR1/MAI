using Microsoft.AspNetCore.Mvc;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Controllers.Abstract;
using TrackingTasksProgressSystem.Services.DTOTransformers;

namespace TrackingTasksProgressSystem.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : BaseController<Employee, EmployeeDTO>
    {
        public EmployeeController(TrackingTasksProgressDbContext dbContext) : base(new EFEmployeeRepository(dbContext),
                                                                                   new EmployeeDTOTransformer(dbContext))
        { }
    }
}
