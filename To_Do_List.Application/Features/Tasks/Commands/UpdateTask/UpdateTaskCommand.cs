using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand
   (
        Guid Id,
        string Title,
        string Description,
        bool IsComplete,
        DateTime DueDate
   ) : IRequest<TaskDto>;
}
