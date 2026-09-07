using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
