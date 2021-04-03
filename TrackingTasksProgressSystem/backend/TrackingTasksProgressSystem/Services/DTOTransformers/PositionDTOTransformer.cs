using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class PositionDTOTransformer : IReadOnlyDtoTranformer<Position, PositionDTO>
    {
        private readonly IReadOnlyDtoTranformer<Department, DepartmentDTO> departmentDTOtransformer;


        public PositionDTOTransformer()
        {
            departmentDTOtransformer = new DepartmentDTOTransformer();
        }


        PositionDTO IReadOnlyDtoTranformer<Position, PositionDTO>.ToDto(Position position)
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
