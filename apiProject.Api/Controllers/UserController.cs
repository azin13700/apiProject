using apiProject.Application.Dtos;
using apiProject.Application.Dtos.Responses;
using apiProject.Application.Services;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;

        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllUserAsync();

            if (result.Count == 0)
                return NotFound("هیچ کاربری ثبت نشده است");

            return Ok(result);
        }

        [HttpPost("CreateUsere")]
        public async Task<IActionResult> Create([FromForm] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int employeeId = await _service.CreateUserAsync(dto);

                return Ok(new
                {
                    success = true,
                    id = employeeId,
                    message = "کاربر با موفقیت ایجاد شد"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "خطا در ایجاد پرسنل",
                    error = ex.Message
                });
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("داده ای یافت نشد");
            }
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUserDto dto)
        {
            try
            {
                if (id != dto.UserId)
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
  

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] SearchUserDto dto)
        {
            if (dto == null)
                return BadRequest("اطلاعات جستجو خالی است");

            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusUserDto dto)
        {
            await _service.ChangeStatus(dto);
            return Ok(new { message = " " });
        }
        
    }
}
