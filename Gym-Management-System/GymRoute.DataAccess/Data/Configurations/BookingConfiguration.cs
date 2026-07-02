using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace GymRoute.DataAccess.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public  void Configure(EntityTypeBuilder<Booking> builder)
    {


        builder.HasOne(b => b.Member)
         .WithMany()
         .HasForeignKey(b => b.MemberId)
         .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Session)
               .WithMany()
               .HasForeignKey(b => b.SessionId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(p=>new { p.MemberId, p.SessionId })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

        builder.HasQueryFilter(x => !x.IsDeleted);

    }
}
