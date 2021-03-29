using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.DTO.ReadOnly;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ShortEmployeeDTOTransformerService : IReadOnlyDtoTranformerService<Employee, ShortEmployeeDTO>
    {
        ShortEmployeeDTO IReadOnlyDtoTranformerService<Employee, ShortEmployeeDTO>.ToDto(Employee employee)
        {
            return new ShortEmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }
    }
}
