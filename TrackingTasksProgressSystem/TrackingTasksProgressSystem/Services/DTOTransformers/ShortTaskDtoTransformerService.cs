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
    public class ShortTaskDtoTransformerService : IReadOnlyDtoTranformerService<Models.Task, ShortTaskDTO>
    {
        private readonly IReadOnlyDtoTranformerService<Status, StatusDTO> statusDtoTransformer;


        public ShortTaskDtoTransformerService()
        {
            statusDtoTransformer = new StatusDTOTransformerService();
        }


        ShortTaskDTO IReadOnlyDtoTranformerService<Models.Task, ShortTaskDTO>.ToDto(Models.Task task)
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
