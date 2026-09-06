
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
        public Guid UserId { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }


        private ToDoTask() { }

        public ToDoTask(string title, string description, bool isCompleted, DateTime dueDate, Guid userId)
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
