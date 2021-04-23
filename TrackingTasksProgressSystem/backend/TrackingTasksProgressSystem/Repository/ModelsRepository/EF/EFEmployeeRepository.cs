using System.Linq;
using System.Collections.Generic;
using TrackingTasksProgressSystem.Models;
using Microsoft.EntityFrameworkCore;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Repository.EF;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.EF
{
    public class EFEmployeeRepository : EFRepositoryCreator<Employee> //, IEmployeeRepository (доп. функционал)
    {
        public EFEmployeeRepository(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }

        
        public override IEnumerable<Employee> GetAll()
        {
            return dbContext.Set<Employee>()
                .Where(employee => employee.PositionId != null) // Работающие в данный момент
                .Include(employee => employee.Position)
                .ThenInclude(position => position.Department);
        }
    }
}
