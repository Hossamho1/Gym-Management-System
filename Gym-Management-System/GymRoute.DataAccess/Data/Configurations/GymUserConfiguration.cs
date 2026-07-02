using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymRoute.DataAccess.Data.Configurations
{
    public class GymUserConfiguration : IEntityTypeConfiguration<GymUser> 
    {
        public virtual  void Configure(EntityTypeBuilder<GymUser> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(100);
             
            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.Property(p => p.phone)
                .HasMaxLength(20);

            builder.OwnsOne<Address>(gu => gu.Address, tp =>
            {
                tp.Property(a => a.Street)
                .HasColumnName("Street ")
                .HasMaxLength(100);

                tp.Property(a => a.City)
                .HasColumnName("City ")
                .HasMaxLength(100);

                tp.Property(a => a.BuildingNumber)
                .HasColumnName("Building number ")
                .HasMaxLength(100);


            });

            builder.ToTable(tp =>
            {
                tp.HasCheckConstraint(
                    "CK_GymUser_Phone",
                    "LEN(Phone) = 11 AND (" +
                    "Phone LIKE '010%' OR " +
                    "Phone LIKE '011%' OR " +
                    "Phone LIKE '012%' OR " +
                    "Phone LIKE '015%')"
                );
            });

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
