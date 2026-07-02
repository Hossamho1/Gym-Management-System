using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore; 

namespace GymRoute.DataAccess.Data.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public  void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasIndex(p => new { p.Email, p.phone })
            .IsUnique();

        builder.Property(p => p.photo)
            .HasMaxLength(200);
        builder.Property(p => p.Email)
            .IsRequired();
        
    }

}
