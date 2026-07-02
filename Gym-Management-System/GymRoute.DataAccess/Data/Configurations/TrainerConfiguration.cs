using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Data.Configurations;

public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{

    void IEntityTypeConfiguration<Trainer>.Configure(EntityTypeBuilder<Trainer> builder)
    {

        builder.Property(t => t.Speciality)
            .HasConversion<string>().HasMaxLength(50);
    }
}
