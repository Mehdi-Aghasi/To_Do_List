
// این کنترلر مسئولیت دریافت درخواست‌های HTTP مربوط به تسک‌ها
// و ارسال آنها به لایه Application از طریق MediatR را بر عهده دارد.
// 
// ویژگی‌های امنیتی
// [Authorize]: تمام endpointها نیاز به احراز هویت دارند
// UserId از توکن JWT استخراج می‌شود امنیت در سطح کاربر
// 
// Endpointها
//  GET /api/Tasks: دریافت لیست تسک‌های کاربر
// GET /api/Tasks/{id}: دریافت جزئیات یک تسک خاص
// POST /api/Tasks: ایجاد تسک جدید
// PUT /api/Tasks/{id}: به‌روزرسانی تسک موجود
// DELETE /api/Tasks/{id}: حذف منطقی تسک (Soft Delete)
// 
// اصول طراحی
// Thin Controller: کنترلر فقط مسئول دریافت درخواست و ارسال پاسخ
// CQRS Pattern: جداسازی Command نوشتن Query خواندن
// MediatR: واسط بین Controller و Application Layer
// RESTful: استفاده از HTTP Methods استاندارد
// ═══════════════════════════════════════════════════════════════

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Application.Common.Dtos;
using To_Do_List.Application.Features.Tasks.Commands.CreateTask;
using To_Do_List.Application.Features.Tasks.Commands.DeleteTask;
using To_Do_List.Application.Features.Tasks.Commands.UpdateTask;
using To_Do_List.Application.Features.Tasks.Queries.GetAllTasks;
using To_Do_List.Application.Features.Tasks.Queries.GetAllTasks;
using To_Do_List.Application.Features.Tasks.Queries.GetByIdTask;

namespace To_Do_List.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll()
        {
            var tasks = await _mediator.Send(new GetAllTaskQuery());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetById(Guid id)
        {
            var task = await _mediator.Send(new GetByIdTaskQuery(id));
            return Ok(task);
        }
        [HttpPost]
        public async Task<ActionResult<TaskDto>> Create(CreateTaskCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            command = command with { UserId = userId };

            var task = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand(id));
            return NoContent();
        }
    }
}