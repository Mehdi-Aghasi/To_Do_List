
// این کنترلر مسئولیت مدیریت عملیات ثبت‌نام و ورود کاربران را
// بر عهده دارد.
// 
// Endpointها
// POST /api/Auth/register: ثبت‌نام کاربر جدید
//    ایجاد کاربر در دیتابیس
//    Hash خودکار پسورد توسط Identity
//    بازگرداندن توکن JWT برای لاگین خودکار
// 
//  POST /api/Auth/login: ورود کاربر موجود
//    بررسی ایمیل و پسورد
//    تولید توکن JWT در صورت موفقیت
//    بازگرداندن توکن و اطلاعات کاربر
// ═══════════════════════════════════════════════════════════════

using MediatR;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Application.Features.Auth.Commands.Login;
using To_Do_List.Application.Features.Auth.Commands.Register;

namespace To_Do_List.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}