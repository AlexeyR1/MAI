using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackingTasksProgressSystem.Models;

namespace TrackingTasksProgressSystem.EFCore.EntityConfigurations
{
    public class PriorityConfiguration : IEntityTypeConfiguration<Priority>
    {
        void IEntityTypeConfiguration<Priority>.Configure(EntityTypeBuilder<Priority> builder)
        {
            builder.ToTable("Priorities");

            builder.HasKey(priority => priority.Id)
                .IsClustered(true);

            builder.Property(priority => priority.Name)
                .HasMaxLength(30)
                .IsUnicode(true)
                .IsRequired(true);

            FillTableWithData(builder);
        }


        private static void FillTableWithData(EntityTypeBuilder<Priority> builder)
        {
            builder.HasData(new Priority(1, "Высокий"),
                            new Priority(2, "Средний"),
                            new Priority(3, "Низкий"));
        }
    }
}
