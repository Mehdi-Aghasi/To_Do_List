
// این اینترفیس قرارداد بین لایه Application و Infrastructure را
// تعریف می‌کند و اصل Dependency Inversion را پیاده‌سازی می‌کند
// 
// متدها
// GetAllTasksAsync(): دریافت همه تسک‌ها با فیلتر Soft Delete
// GetTaskByIdAsync(): دریافت تسک بر اساس شناسه یکتا
// AddTaskAsync(): افزودن تسک جدید به دیتابیس
// UpdateTaskAsync(): به‌روزرسانی تسک موجود
// ═══════════════════════════════════════════════════════════════

using To_Do_List.Domain.Entities;

namespace To_Do_List.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        Task<ToDoTask> GetTaskByIdAsync(Guid id);
        Task<ToDoTask> CreateTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
        Task DeleteTaskAsync(Guid id);
    }
}
