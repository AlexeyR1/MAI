using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        void IEntityTypeConfiguration<Status>.Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Statuses");

            builder.HasKey(status => status.Id)
                .IsClustered(true);

            builder.Property(status => status.Name)
                .HasMaxLength(30)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
