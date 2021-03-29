using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class PositionDTOTransformerService : IReadOnlyDtoTranformerService<Position, PositionDTO>
    {
        private readonly IReadOnlyDtoTranformerService<Department, DepartmentDTO> departmentDTOtransformer;


        public PositionDTOTransformerService()
        {
            departmentDTOtransformer = new DepartmentDTOTransformerService();
        }


        PositionDTO IReadOnlyDtoTranformerService<Position, PositionDTO>.ToDto(Position position)
        {
            return new PositionDTO
            {
                Id = position.Id,
                Name = position.Name,
                Department = departmentDTOtransformer.ToDto(position.Department)
            };
        }
    }
}
