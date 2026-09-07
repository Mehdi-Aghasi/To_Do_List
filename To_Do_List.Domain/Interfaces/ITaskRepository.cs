using To_Do_List.Domain.Entities;

namespace To_Do_List.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoTask>> GetAllTasksAsync();
        Task<ToDoTask> GetTaskByIdAsync(Guid id);
        Task<ToDoTask> CreateTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
        Task DeleteTaskAsync(ToDoTask task);
    }
}
