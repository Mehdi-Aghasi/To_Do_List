using MediatR;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Features.Tasks.Queries.GetByIdTask
{
    public class GetTaskByIdHandler : IRequestHandler<GetByIdTaskQuery, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(GetByIdTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(request.id);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found");
            }

            return new TaskDto(
                task.Id,
                task.Title,
                task.Description,
                task.IsCompleted,
                task.DueDate);
        }
    }
}
