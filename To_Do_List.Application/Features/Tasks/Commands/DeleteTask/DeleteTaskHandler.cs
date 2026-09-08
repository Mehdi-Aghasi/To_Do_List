using MediatR;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.Id);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            task.MarkAsDeleted();
            await _taskRepository.UpdateTaskAsync(task);
            return new TaskDto(task.Id,task.Title,task.Description,task.IsCompleted,task.DueDate);
        }
    }
}
