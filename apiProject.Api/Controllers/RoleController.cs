using apiProject.Application.Dtos.Role;
using apiProject.Application.Services;
using apiProject.Application.Services.Interface;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;

        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (result.Count == 0)
                return NotFound("هیچ نقشی ثبت نشده است");

            return Ok(result);
        }



        // دریافت نقش با شناسه

        [HttpGet("getrolebyid/{id}")]
        public async Task<IActionResult> getrolebyid(int id)
        {
            try
            {
                var role = await _service.GetByIdAsync(id);
                if (role == null)
                    return NotFound(new { message = "نقش یافت نشد" });

                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateRoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int employeeId = await _service.CreateAsync(dto);

                return Ok(new
                {
                    success = true,
                    id = employeeId,
                    message = "نقش با موفقیت ایجاد شد"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "خطا در ایجاد نقش",
                    error = ex.Message
                });
            }
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateRoleDto dto)
        {
            try
            {
                if (id != dto.RoleId)
                    return BadRequest(new { message = "شناسه کاربر مطابقت ندارد" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateAsync(dto);
                return Ok(new { success = true, message = "کاربر با موفقیت به‌روزرسانی شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusRoleDto dto)
        {
            await _service.ChangeStatus(dto);
            return Ok(new { message = " " });
        }


    }
}

