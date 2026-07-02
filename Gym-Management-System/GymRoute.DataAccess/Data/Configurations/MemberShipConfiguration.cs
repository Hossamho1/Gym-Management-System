using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Data.Configurations;

public class MemberShipConfiguration : IEntityTypeConfiguration<MemberShip>
{
    public  void Configure(EntityTypeBuilder<MemberShip> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted)
               .HasDefaultValue(false); 

        builder.Property(x => x.StartDate)
               .IsRequired();

        builder.Property(x => x.EndDate)
               .IsRequired();


        builder.HasOne(x => x.Member)
               .WithMany() 
               .HasForeignKey(x => x.MemberId)
               .OnDelete(DeleteBehavior.Restrict); 

      
        builder.HasOne(x => x.Plan)
               .WithMany()  
               .HasForeignKey(x => x.PlanId)
               .OnDelete(DeleteBehavior.Restrict);

        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
