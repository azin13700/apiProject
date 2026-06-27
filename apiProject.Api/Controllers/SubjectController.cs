using apiProject.Application.Dtos.Responses;
using apiProject.Application.Dtos.Subject;
using apiProject.Application.Dtos.Unit;
using apiProject.Application.Services;
using apiProject.Application.Services.Interface;
using apiProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;
        public SubjectController(ISubjectService service)
        {
            _service = service;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSubjectAsync();

            if (result.Count == 0)
                return NotFound("هیچ نقشی ثبت نشده است");

            return Ok(result);

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSubjectDto dto)
        {
            var result = await _service.CreateSubjectAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSubjectDto dto)
        {
            if (id != dto.SubjectId)
                return BadRequest(new { message = "شناسه موضوع مطابقت ندارد" });

            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest(new { message = "عنوان الزامی است" });

            await _service.UpdateAsync(id, dto);

            return Ok(new
            {
                success = true,
                message = "موضوع با موفقیت ویرایش شد"
            });
        }

        [HttpGet("{parentId}/children")]
        public async Task<IActionResult> GetChildren(int parentId)
        {
            var result = await _service.GetSubSubjectByIdAsync(parentId);

            return Ok(result);
        }

        [HttpGet("{subjectId}/subsubjects")]
        public async Task<IActionResult> GetSubSubjects(int subjectId)
        {
            var result = await _service.GetSubSubjectForRequest(subjectId);

            return Ok(result);
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatusSubjectDto dto)
        {
            await _service.ChangeStatus(dto);
            return Ok(new { message = " " });
        }
  
    }
}
