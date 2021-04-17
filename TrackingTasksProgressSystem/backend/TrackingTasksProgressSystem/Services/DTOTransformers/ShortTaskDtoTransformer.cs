using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.DTO.ReadOnly;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class ShortTaskDtoTransformer : IReadOnlyDtoTranformer<Models.Task, ShortTaskDTO>
    {
        private readonly IReadOnlyDtoTranformer<Status, StatusDTO> statusDtoTransformer;
        private readonly IReadOnlyDtoTranformer<Employee, ShortEmployeeDTO> employeeDtoTransformer;


        public ShortTaskDtoTransformer()
        {
            statusDtoTransformer = new StatusDTOTransformer();
            employeeDtoTransformer = new ShortEmployeeDTOTransformer();
        }


        ShortTaskDTO IReadOnlyDtoTranformer<Models.Task, ShortTaskDTO>.ToDto(Models.Task task)
        {
            return new ShortTaskDTO
            {
                Id = task.Id,
                Summary = task.Summary,
                Status = statusDtoTransformer.ToDto(task.Status),
                PerformingBy = employeeDtoTransformer.ToDto(task.PerformingBy)
            };
        }
    }
}
