
using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace GymRoute.DataAccess.Data.Contexts;


public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
       


            builder.Property(b=> b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                         .IsRequired()
                         .HasMaxLength(200);
            builder.Property(b => b.Price)
                .IsRequired()
                .HasPrecision(18, 2);


            builder.Property(b => b.DurationDays)
                .IsRequired();
            builder.ToTable(tb => {
                tb.HasCheckConstraint("CHK_Plan_DurationDays", "DurationDays BETWEEN 1 AND 365");
            }
            );


            builder.HasIndex(b => b.Name).IsUnique();
        builder.HasQueryFilter(x => !x.IsDeleted);



    }
}

