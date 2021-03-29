using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingTasksProgressSystem.Repository.EF;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.EFCore;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.EF
{
    public class EFPositionRepository : EFRepositoryDeleter<Position>
    {
        public EFPositionRepository(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }

        public override IEnumerable<Position> GetAll()
        {
            return dbContext.Set<Position>()
                .Include(position => position.Department)
                .AsEnumerable();
        }
    }
}
