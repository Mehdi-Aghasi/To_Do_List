using FluentValidation;

namespace To_Do_List.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskValidator:AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.DueDate).GreaterThan(DateTime.UtcNow);
        }
    }
}
