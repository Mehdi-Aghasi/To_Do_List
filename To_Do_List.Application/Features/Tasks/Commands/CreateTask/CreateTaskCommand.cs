
// این رکورد نمایانگر یک درخواست برای ایجاد تسک جدید است

// فیلدها:
//  Title: عنوان تسک اجباری حداکثر 300 کاراکتر
// Description: توضیحات تسک اختیاری حداکثر 1000 کاراکتر)
// DueDate: تاریخ سررسید (باید در آینده باشد
// ══════════════════════════════════════════════════════════════

using MediatR;
using To_Do_List.Application.Common.Dtos;

namespace To_Do_List.Application.Features.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand
    (
        string Title,
        string? Description,
        DateTime DueDate,
        string UserId
    ) : IRequest<TaskDto>;
}
