using MediatR;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new ToDoTask(
                request.Title,
                request.Description,
                false,
                request.DueDate,
                Guid.NewGuid().ToString()
                );

            var createTask=await _taskRepository.CreateTaskAsync(task);

            return new TaskDto(
                createTask.Id,
                createTask.Title,
                createTask.Description,
                createTask.IsCompleted,
                createTask.DueDate
                );
        }
    }
}
