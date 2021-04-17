using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class EmployeeDTOTransformer : IDtoTranformer<Employee, EmployeeDTO>
    {
        private readonly IRepositoryReader<Position> positionRepository;
        private readonly IReadOnlyDtoTranformer<Position, PositionDTO> positionDTOTransformer;


        public EmployeeDTOTransformer(TrackingTasksProgressDbContext dbContext)
        {
            positionRepository = new EFPositionRepository(dbContext);
            positionDTOTransformer = new PositionDTOTransformer();
        }


        Employee IDtoTranformer<Employee, EmployeeDTO>.FromDto(EmployeeDTO dto)
        {
            return new Employee(dto.FirstName,
                                dto.LastName,
                                positionRepository.GetById(dto.Position.Id),
                                dto.Email);
        }


        EmployeeDTO IReadOnlyDtoTranformer<Employee, EmployeeDTO>.ToDto(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = positionDTOTransformer.ToDto(employee.Position),
                Email = employee.Email
            };
        }
    }
}
