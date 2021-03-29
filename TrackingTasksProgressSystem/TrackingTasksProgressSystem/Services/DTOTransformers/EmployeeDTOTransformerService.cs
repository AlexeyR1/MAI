using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.DTO;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.Abstract;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;
using TrackingTasksProgressSystem.Services.DTOTransformers.Abstract;

namespace TrackingTasksProgressSystem.Services.DTOTransformers
{
    public class EmployeeDTOTransformerService : IDtoTranformerService<Employee, EmployeeDTO>
    {
        private readonly IRepositoryBase<Position> positionRepository;
        private readonly IReadOnlyDtoTranformerService<Position, PositionDTO> positionDTOTransformer;


        public EmployeeDTOTransformerService(TrackingTasksProgressDbContext dbContext)
        {
            positionRepository = new EFPositionRepository(dbContext);
            positionDTOTransformer = new PositionDTOTransformerService();
        }


        Employee IDtoTranformerService<Employee, EmployeeDTO>.FromDto(EmployeeDTO dto)
        {
            return new Employee(dto.FirstName,
                                dto.LastName,
                                positionRepository.GetById(dto.Position.Id),
                                dto.Email,
                                dto.Password);
        }


        EmployeeDTO IReadOnlyDtoTranformerService<Employee, EmployeeDTO>.ToDto(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = positionDTOTransformer.ToDto(employee.Position),
                Email = employee.Email,
                Password = employee.Password
            };
        }
    }
}
