using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Models.Intermediate;

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
                .WithMany(employee => employee.AuthorBy)
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
               .WithMany(attachment => attachment.ToProblems)
               .UsingEntity<TasksProblemAttachments>(
                   join => join.HasOne(tasksProblemAttachments => tasksProblemAttachments.Attachment)
                               .WithMany(task => task.TasksProblemAttachments)
                               .HasForeignKey(tasksResponseAttachments => tasksResponseAttachments.AttachmentId)
                               .IsRequired(false)
                               .OnDelete(DeleteBehavior.Cascade),
                   join => join.HasOne(tasksProblemAttachments => tasksProblemAttachments.Task)
                               .WithMany(attachment => attachment.TasksProblemAttachments)
                               .HasForeignKey(tasksResponseAttachments => tasksResponseAttachments.TaskId)
                               .IsRequired(true)
                               .OnDelete(DeleteBehavior.Cascade),
                   join =>
                   {
                       join.ToTable("TasksProblem_Attachments");
                       join.HasKey(tasksResponseAttachments => tasksResponseAttachments.Id);
                   });

            builder.HasMany(task => task.ResponseAttachments)
                .WithMany(attachment => attachment.ToResponses)
                .UsingEntity<TasksResponseAttachments>(
                    join => join.HasOne(tasksResponseAttachments => tasksResponseAttachments.Attachment)
                                .WithMany(task => task.TasksResponseAttachments)
                                .HasForeignKey(tasksResponseAttachments => tasksResponseAttachments.AttachmentId)
                                .IsRequired(false)
                                .OnDelete(DeleteBehavior.Cascade),
                    join => join.HasOne(tasksResponseAttachments => tasksResponseAttachments.Task)
                                .WithMany(attachment => attachment.TasksResponseAttachments)
                                .HasForeignKey(tasksResponseAttachments => tasksResponseAttachments.TaskId)
                                .IsRequired(true)
                                .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("TasksResponse_Attachments");
                        join.HasKey(tasksResponseAttachments => tasksResponseAttachments.Id);
                    });

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
