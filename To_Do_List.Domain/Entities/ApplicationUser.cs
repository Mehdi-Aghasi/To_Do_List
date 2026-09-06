using Microsoft.AspNetCore.Identity;

namespace To_Do_List.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
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