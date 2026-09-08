
// این کلاس یک MediatR Pipeline Behavior است که به صورت خودکار
// قبل از اجرای هر Handler، اعتبارسنجی را انجام می‌دهد.
// 
// نحوه کار
//  MediatR قبل از ارسال Command به Handler این Behavior را اجرا می‌کند
// Behavior تمام Validatorهای ثبت‌شده برای آن Command را پیدا می‌کند
//  اعتبارسنجی را به صورت asynchronous انجام می‌دهد
// در صورت خطا، ValidationException پرتاب می‌کند
// در صورت موفقیت، درخواست را به Handler بعدی ارسال می‌کند
// 
// ثبت در DI
// services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
// ═══════════════════════════════════════════════════════════════

using FluentValidation;
using MediatR;

namespace To_Do_List.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}