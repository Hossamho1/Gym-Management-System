using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Data.Configurations;

public class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
{
    public void Configure(EntityTypeBuilder<HealthRecord> builder)
    {
        builder.Property(x => x.Height)
        .HasPrecision(5, 2);

        builder.Property(x => x.Weight)
            .HasPrecision(5, 2);

        builder.Property(x => x.BloodType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000);

        builder.HasOne(h => h.Member)
            .WithOne(m => m.HealthRecord)
            .HasForeignKey<HealthRecord>(x => x.MemberId);

        builder.HasQueryFilter(x => !x.IsDeleted);

    }
}
