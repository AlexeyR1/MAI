using System.Collections.Generic;
using System.Linq;
using TrackingTasksProgressSystem.EFCore;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.Repository.EF;

namespace TrackingTasksProgressSystem.Repository.ModelsRepository.EF
{
    public class EFResponseAttachmentRepository : EFRepositoryBase<BaseAttachment>
    {
        public EFResponseAttachmentRepository(TrackingTasksProgressDbContext dbContext) : base(dbContext) { }

        public override IEnumerable<BaseAttachment> GetAll()
        {
            return dbContext.ResponseAttachments.AsEnumerable();
        }
    }
}
