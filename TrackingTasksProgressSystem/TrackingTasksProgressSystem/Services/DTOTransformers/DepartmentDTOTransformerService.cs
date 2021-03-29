using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class DepartmentDTOTransformerService : IReadOnlyDtoTranformerService<Department, DepartmentDTO>
    {
        DepartmentDTO IReadOnlyDtoTranformerService<Department, DepartmentDTO>.ToDto(Department department)
        {
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}
