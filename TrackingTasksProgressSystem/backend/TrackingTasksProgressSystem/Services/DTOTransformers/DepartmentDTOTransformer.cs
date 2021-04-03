using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class DepartmentDTOTransformer : IReadOnlyDtoTranformer<Department, DepartmentDTO>
    {
        DepartmentDTO IReadOnlyDtoTranformer<Department, DepartmentDTO>.ToDto(Department department)
        {
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}
