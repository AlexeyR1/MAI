using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;
using TrackingTasksProgressSystem.Repository.ModelsRepository.Abstract;
using TrackingTasksProgressSystem.Repository.ModelsRepository.EF;

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

            FillTableWithData(builder);
        }


        private static void FillTableWithData(EntityTypeBuilder<Position> builder)
        {
            // Магические цифры
            builder.HasData(new Position(1, "Технический специалист", 1),
                            new Position(2, "Инженер по качеству", 2),
                            new Position(3, "Младший разработчик", 4),
                            new Position(4, "Старший разработчик", 4),
                            new Position(5, "Руководитель проекта", 3));
        }
    }
}
