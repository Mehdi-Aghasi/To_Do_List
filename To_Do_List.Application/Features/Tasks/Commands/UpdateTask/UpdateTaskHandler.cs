using MediatR;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand,TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
           var task=await _taskRepository.GetTaskByIdAsync(request.Id);

            if(task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            task.Update(request.Title, request.Description, request.IsComplete, request.DueDate);
            await _taskRepository.UpdateTaskAsync(task);

            return new TaskDto(task.Id,task.Title,task.Description,task.IsCompleted,task.DueDate);
        }
    }
}
