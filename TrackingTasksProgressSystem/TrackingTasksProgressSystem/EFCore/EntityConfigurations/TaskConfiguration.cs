using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Models.Task>
    {
        void IEntityTypeConfiguration<Models.Task>.Configure(EntityTypeBuilder<Models.Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(task => task.Id)
                .IsClustered(true);

            builder.HasOne(task => task.Status)
                .WithMany(status => status.Tasks)
                .HasForeignKey(task => task.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(task => task.Author)
                .WithMany(employee => employee.CreatedTasks)
                .HasForeignKey(task => task.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(task => task.PerformingBy)
                .WithMany(employee => employee.PerformingTasks)
                .HasForeignKey(task => task.PerformingById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(task => task.Priority)
                .WithMany(priority => priority.Tasks)
                .HasForeignKey(task => task.PriorityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(task => task.ProblemAttachments)
               .WithOne(attachment => attachment.Task)
               .HasForeignKey(attachment => attachment.TaskId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(task => task.ResponseAttachments)
                .WithOne(responseAttachment => responseAttachment.Task)
                .HasForeignKey(responseAttachment => responseAttachment.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(task => task.Summary)
                .HasMaxLength(150)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(task => task.StatusId)
                .IsRequired(true);

            builder.Property(task => task.AuthorId)
                .IsRequired(true);

            builder.Property(task => task.PerformingById)
                .IsRequired(true);

            builder.Property(task => task.PriorityId)
                .IsRequired(true);

            builder.Property(task => task.CreatedAt)
                .IsRequired(true);

            builder.Property(task => task.ProblemAnnotation)
                .HasMaxLength(2000)
                .IsUnicode(true)
                .IsRequired(false);

            builder.Property(task => task.ResponseAnnotation)
                 .HasMaxLength(2000)
                .IsUnicode(true)
                .IsRequired(false);
        }
    }
}
