
// این کلاس نمایانگر یک تسک (وظیفه) در سیستم مدیریت تسکه
// به عنوان یک Aggregate Root در الگوی Domain-Driven Design 
// ویژگی‌های 
// استفاده از کپسوله سازی در طراحی مدل و فیلد ها تا کاربر نتونه مسقیم به فیلد ها دسترسی داشته باشه 
//تعریق فیلد isdeleted برای انحام عملیات soft Delete
// 
// اصول طراحی
//  Rich Domain Model: منطق کسب‌وکار در خود مدل قرار دارد
//  Anemic Model avoidance: مدل فقط داده نیست، رفتار هم دارد
// ═══════════════════════════════════════════════════════════════

namespace To_Do_List.Domain.Entities
{
    public class ToDoTask
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsDeleted { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }

        public void Update(string title, string? description, bool isCompleted, DateTime dueDate)
        {
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            DueDate = dueDate;
        }

        private ToDoTask() { }

        public ToDoTask(string title, string description, bool isCompleted, DateTime dueDate, string userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            DueDate = dueDate;
            IsDeleted = false;
            UserId = userId;
        }
    }
}
