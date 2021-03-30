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
    public class ResponseAttachmentConfiguration : BaseAttachmentConfiguration<ResponseAttachment>,
                                                   IEntityTypeConfiguration<ResponseAttachment>
    {
        void IEntityTypeConfiguration<ResponseAttachment>.Configure(EntityTypeBuilder<ResponseAttachment> builder)
        {
            Configure(builder);
        }


        private protected override void Configure(EntityTypeBuilder<ResponseAttachment> builder)
        {
            builder.ToTable("ResponseAttachments");
            base.Configure(builder);
        }
    }
}
