
// این کلاس با ارث‌بری از IdentityUser، قابلیت‌های احراز هویت
// پیش‌فرض ASP.NET Core را گسترش می‌دهد
// 
// فیلدای اضافه شده:
// FirstName
// LastName
// CreatedAt
// رابطع One-to-Many با ToDoTask (هر کاربر چندین تسک دارد)
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Identity;

namespace To_Do_List.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<ToDoTask> ToDoTasks { get; private set; }

        private ApplicationUser() { }

        public ApplicationUser(string name, string family)
        {
            FirstName = name;
            LastName = family;
            CreatedAt = DateTime.UtcNow;
            ToDoTasks = new List<ToDoTask>();
        }
    }
}