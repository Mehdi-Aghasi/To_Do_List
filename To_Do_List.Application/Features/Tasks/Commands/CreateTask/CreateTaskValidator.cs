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
