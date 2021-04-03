using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class PriorityDTOTransformer : IReadOnlyDtoTranformer<Priority, PriorityDTO>
    {
        PriorityDTO IReadOnlyDtoTranformer<Priority, PriorityDTO>.ToDto(Priority priority)
        {
            return new PriorityDTO
            {
                Id = priority.Id,
                Name = priority.Name
            };
        }
    }
}
