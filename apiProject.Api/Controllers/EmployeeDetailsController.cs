using apiProject.Application.Dtos;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDetailsController : Controller
    {
        private readonly IEmployeeService _service;
        public EmployeeDetailsController(IEmployeeService service)
        {
            _service = service;

        }
        [HttpPost("CreateDetailsEmployee")]
        public async Task<IActionResult> CreateDetails([FromBody] CreateEmployeeDetailsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int employeeId = await _service.CreateDetailsAsync(dto);

            return Ok(new { id = employeeId, message = "کارمند با موفقیت ایجاد شد" });


        }
        [HttpPut("UpdateDetailsEmployee")]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateEmployeeDetailsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int employeeId = await _service.UpdateDetailsAsync(dto);

            return Ok(new { id = employeeId, message = "کارمند با موفقیت ایجاد شد" });


        }

    }
}
