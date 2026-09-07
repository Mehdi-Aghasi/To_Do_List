using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;
using To_Do_List.Infrastructure.Data;

namespace To_Do_List.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync()
        {
           return await _context.ToDoTasks.AsNoTracking().ToListAsync();
        }

        public async Task<ToDoTask> GetTaskByIdAsync(Guid id)
        {
            return await _context.ToDoTasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ToDoTask> CreateTaskAsync(ToDoTask task)
        {
            var getTask= await _context.ToDoTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return getTask.Entity;
        }

        public async Task UpdateTaskAsync(ToDoTask task)
        {
            _context.ToDoTasks.Update(task);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteTaskAsync(Guid id)
        {
           var task= await _context.ToDoTasks.FindAsync(id);
            if(task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }
            task.MarkAsDeleted();
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

    }
}
