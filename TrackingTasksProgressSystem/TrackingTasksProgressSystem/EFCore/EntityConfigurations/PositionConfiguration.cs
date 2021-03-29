using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        void IEntityTypeConfiguration<Position>.Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("Positions");

            builder.HasKey(position => position.Id)
                .IsClustered(true);

            builder.HasOne(position => position.Department)
                .WithMany(department => department.Positions)
                .HasForeignKey(position => position.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(position => position.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(position => position.DepartmentId)
                .IsRequired(true);
        }
    }
}
