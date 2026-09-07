using MediatR;

namespace To_Do_List.Application.Common.Dtos
{
    public record TaskDto
    (
        Guid Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime DueDate
    );
}
