using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand
    (
        string Title,
        string? Description,
        DateTime DueDate
    ) : IRequest<TaskDto>;
}
