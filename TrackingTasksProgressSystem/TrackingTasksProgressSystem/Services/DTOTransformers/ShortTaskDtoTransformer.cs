using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.DTO.ReadOnly;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ShortTaskDtoTransformer : IReadOnlyDtoTranformer<Models.Task, ShortTaskDTO>
    {
        private readonly IReadOnlyDtoTranformer<Status, StatusDTO> statusDtoTransformer;


        public ShortTaskDtoTransformer()
        {
            statusDtoTransformer = new StatusDTOTransformer();
        }


        ShortTaskDTO IReadOnlyDtoTranformer<Models.Task, ShortTaskDTO>.ToDto(Models.Task task)
        {
            return new ShortTaskDTO
            {
                Id = task.Id,
                Summary = task.Summary,
                Status = statusDtoTransformer.ToDto(task.Status)
            };
        }
    }
}
