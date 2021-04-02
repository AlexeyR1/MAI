using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Models.Abstract;
using TrackingTasksProgressSystem.EFCore.EntityConfigurations.Abstract;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class AttachmentConfiguration : BaseAttachmentConfiguration<ProblemAttachment>, IEntityTypeConfiguration<ProblemAttachment>
    {
        void IEntityTypeConfiguration<ProblemAttachment>.Configure(EntityTypeBuilder<ProblemAttachment> builder)
        {
            Configure(builder);
        }


        private protected override void Configure(EntityTypeBuilder<ProblemAttachment> builder)
        {
            builder.ToTable("ProblemAttachments");
            base.Configure(builder);
        }
    }
}
