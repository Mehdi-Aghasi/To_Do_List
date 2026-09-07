using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Queries.GetAllTasks
{
    public record GetAllTaskQuery() : IRequest<IEnumerable<TaskDto>>;
}
