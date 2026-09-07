using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Infrastructure.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            builder.ToTable("Tasks");
            builder.HasKey(key => key.Id);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("nvarchar(300)")
                .HasMaxLength(300);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(1000)")
                .HasMaxLength(1000);

            builder.HasOne(u => u.ApplicationUser)
                .WithMany(t => t.ToDoTasks)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
