using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        void IEntityTypeConfiguration<Employee>.Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(employee => employee.Id)
                .IsClustered(true);

            builder.HasOne(employee => employee.Position)
                .WithMany(position => position.Employees)
                .HasForeignKey(employee => employee.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(employee => employee.FirstName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);          
            
            builder.Property(employee => employee.LastName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(employee => employee.PositionId)
                .IsRequired(false);
            
            builder.Property(employee => employee.Email)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);         
            
            builder.Property(employee => employee.Password)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);
        }
    }
}
