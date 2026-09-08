
// این کلاس مسئولیت مدیریت ارتباط با دیتابیس و پیکربندی Entityها
// را بر عهده دارد
// 
// Picket Configuration: پیکربندی دقیق Entityها
// تعریف Primary Key، Foreign Key
// تنظیم طول فیلدها و محدودیت‌ه
// پیکربندی روابط بین Entityها
// 
// Identity Integration: یکپارچگی با ASP.NET Core Identity
// جداول کاربران، نقش‌ها و claimها
// مدیریت Session و Token
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Infrastructure.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
