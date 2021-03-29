using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        void IEntityTypeConfiguration<Department>.Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(department => department.Id)
                .IsClustered(true);

            builder.Property(department => department.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
