using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Queries.GetByIdTask
{
    public record GetByIdTaskQuery(Guid id) : IRequest<TaskDto>;
}
