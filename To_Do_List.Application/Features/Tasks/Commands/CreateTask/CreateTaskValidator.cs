
// این کلاس با استفاده از FluentValidation، قوانین اعتبارسنجی
// را برای CreateTaskCommand تعریف می‌کند.
// 
// قوانین اعتبارسنجی
// Title: نباید خالی باشد، حداکثر 300 کاراکتر
// Description: اختیاری، حداکثر 1000 کاراکتر
// DueDate: باید در آینده باشد جلوگیری از تسک‌های منقضی
// UserId: نباید خالی باشد باید کاربر لاگین کرده باشد
// 
// یکپارچگی با MediatR
//  ValidationBehavior به صورت خودکار Validator را اجرا می‌کند
//  در صورت خطا، ValidationException پرتاب می‌شود
// Handler فقط در صورت اعتبارسنجی موفق اجرا می‌شود
// ═══════════════════════════════════════════════════════════════

using FluentValidation;

namespace To_Do_List.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskValidator:AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("عنوان تسک نمی‌تواند خالی باشد")
                .MaximumLength(300).WithMessage("عنوان تسک نمی‌تواند بیشتر از ۳۰ کاراکتر باشد");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("توضیحات نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("تاریخ سررسید باید آینده باشد");
        }
    }
}
