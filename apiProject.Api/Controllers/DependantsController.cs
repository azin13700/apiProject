
using apiProject.Application.Dtos;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace apiProject.Api.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class DependantsController : ControllerBase
{
    private readonly IDependantService _service;

    public DependantsController(IDependantService service)
    {
        _service = service;
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployeeId(int employeeId)
    {
        var result = await _service.GetByEmployeeIdAsync(employeeId);
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
    public async Task<IActionResult> Create(CreateDependantDto dto)
    {
        var id = await _service.CreateAsync(dto);

        return Ok(new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDependantDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return Ok("Updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok("Deleted");
    }
}
}