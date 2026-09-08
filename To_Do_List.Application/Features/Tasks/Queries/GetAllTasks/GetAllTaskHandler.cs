using MediatR;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Features.Tasks.Queries.GetAllTasks
{
    public class GetAllTaskHandler : IRequestHandler<GetAllTaskQuery, IEnumerable<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetAllTasksAsync();

            return tasks.Select(t => new TaskDto(
              t.Id,
              t.Title,
              t.Description,
              t.IsCompleted,
              t.DueDate));
        }
    }
}
