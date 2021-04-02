using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class StatusDTOTransformer : IReadOnlyDtoTranformer<Status, StatusDTO>
    {
        StatusDTO IReadOnlyDtoTranformer<Status, StatusDTO>.ToDto(Status status)
        {
            return new StatusDTO
            {
                Id = status.Id,
                Name = status.Name
            };
        }
    }
}
