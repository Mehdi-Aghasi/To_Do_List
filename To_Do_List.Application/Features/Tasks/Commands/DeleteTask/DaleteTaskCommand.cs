using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(Guid Id) : IRequest<TaskDto>;
}
