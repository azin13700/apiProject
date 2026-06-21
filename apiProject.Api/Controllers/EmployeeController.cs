using apiProject.Application.Dtos;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using apiProject.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
                
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            if (result.Count == 0)
                return NotFound("هیچ کارمندی ثبت نشده است");

            return Ok(result);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetById( int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("داده ای یافت نشد");
            }
            return Ok(result);
        }
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeDto dto)
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
                    message = "کارمند با موفقیت ایجاد شد"
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


        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> Update(UpdateEmployeeDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok(new { id = dto.EmployeeId, message = "کارمند با موفقیت ایجاد شد" });

        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "پرسنل با موفقیت حذف شد" });
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] SearchEmployeeDto dto)
        {
            if (dto == null)
                return BadRequest("اطلاعات جستجو خالی است");

            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }
        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusDto dto)
        {
            await _service.ChangeStatus(dto);
            return Ok(new { message = " " });
        }
        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoUploadDto dto)
        {
            if (dto.File == null)
                return BadRequest("هیچ فایلی انتخاب نشده است");

            var result = await _service.UploadPhotoAsync(dto);

            return result
                ? Ok(new { message = "عکس با موفقیت آپلود شد" })
                : BadRequest("آپلود عکس با مشکل مواجه شد");
        }

    }
}
