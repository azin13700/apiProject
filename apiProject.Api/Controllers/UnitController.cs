using apiProject.Application.Dtos;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits()
        {
            try
            {
                var units = await _unitService.GetAllAsync();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateUnitDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                int employeeId = await _unitService.CreateAsync(dto);

                return Ok(new
                {
                    success = true,
                    id = employeeId,
                    message = "واحد با موفقیت ایجاد شد"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "خطا در ایجاد واحد",
                    error = ex.Message
                });
            }
        }

        [HttpGet("GetUnitById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("داده ای یافت نشد");
            }
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateUnitDto dto)
        {
            try
            {
                if (id != dto.UnitId)
                    return BadRequest(new { message = "شناسه کاربر مطابقت ندارد" });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _unitService.UpdateAsync(dto);
                return Ok(new { success = true, message = "کاربر با موفقیت به‌روزرسانی شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusUnitDto dto)
        {
            await _unitService.ChangeStatus(dto);
            return Ok(new { message = " " });
        }
    }
}
