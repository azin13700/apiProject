using apiProject.Application.Dtos.Request;
using apiProject.Application.Dtos.Subject;
using apiProject.Application.Dtos.User;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : Controller
    {
        private readonly IRequestService _service;
        public RequestController(IRequestService service)
        {
            _service = service;

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRequestDto dto)
        {
            var result = await _service.CreateRequestAsync(dto);
            return Ok(new
            {
                success = true,
                id = result.RequestId,
                requestCode = result.RequestCode,
                message = "درخواست با موفقیت ایجاد شد"
            });
        }


        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] SearchRequestDto dto)
        {
            if (dto == null)
                return BadRequest("اطلاعات جستجو خالی است");

            var result = await _service.SearchAsync(dto);
            return Ok(result);
        }

        [HttpGet("work-flow/{userId}")]
        public async Task<IActionResult> GetWorkFlow(int userId)
        {
            var result = await _service.GetRequestsByUserIdAsync(userId);

            return Ok(result);
        }
    }
}
