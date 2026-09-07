using FluentValidation;

namespace To_Do_List.Application.Features.Tasks.Commands.DeleteTask
{
    public class DeleteTaskValidator:AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskValidator() 
        { 
            RuleFor(x=>x.Id).NotEmpty().WithMessage("شناسه تسک نمی‌تواند خالی باشد");
        }
    }
}
