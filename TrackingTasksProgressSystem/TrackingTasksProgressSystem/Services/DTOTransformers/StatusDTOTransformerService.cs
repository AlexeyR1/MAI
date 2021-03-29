using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class StatusDTOTransformerService : IReadOnlyDtoTranformerService<Status, StatusDTO>
    {
        StatusDTO IReadOnlyDtoTranformerService<Status, StatusDTO>.ToDto(Status status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }
    }
}
