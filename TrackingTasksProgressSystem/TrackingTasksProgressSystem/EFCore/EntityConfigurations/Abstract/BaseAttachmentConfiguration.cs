using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models.Abstract;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations.Abstract
{
    public abstract class BaseAttachmentConfiguration<T> where T : BaseAttachment
    {
        private virtual protected void Configure(EntityTypeBuilder<T> builder)
        {
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
