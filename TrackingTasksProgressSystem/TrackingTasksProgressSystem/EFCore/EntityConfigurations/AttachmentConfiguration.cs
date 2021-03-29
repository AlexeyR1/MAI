using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        void IEntityTypeConfiguration<Attachment>.Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");

            builder.HasKey(attachment => attachment.Id)
                .IsClustered(true);

            builder.Property(attachment => attachment.Name)
                .HasMaxLength(256)
                .IsUnicode(true)
                .IsRequired(true);       
            
            builder.Property(attachment => attachment.Data)
                .IsRequired(true);   
            
            builder.Property(attachment => attachment.CreatedAt)
                .IsRequired(true);
        }
    }
}
