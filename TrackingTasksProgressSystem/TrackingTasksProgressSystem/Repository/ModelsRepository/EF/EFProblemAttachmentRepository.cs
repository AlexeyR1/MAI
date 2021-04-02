using System.Collections.Generic;
using System.Linq;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.EF;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.EF
{
    public class EFProblemAttachmentRepository : EFRepositoryBase<BaseAttachment>
    {
        public EFProblemAttachmentRepository(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }

        public override IEnumerable<BaseAttachment> GetAll()
        {
            return dbContext.ProblemAttachments.AsEnumerable();
        }
    }
}
