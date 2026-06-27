using apiProject.Application.Dtos.Permission;
using apiProject.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace apiProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAllAsync();
            return Ok(permissions);
        }

        // GET: api/Permission/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);
            if (permission == null)
                return NotFound(new { message = "دسترسی یافت نشد" });

            return Ok(permission);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = await _permissionService.CreateAsync(dto);
                return Ok(new { success = true, id = id, message = "دسترسی با موفقیت ایجاد شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // PUT: api/Permission/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePermissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _permissionService.UpdateAsync(dto);
                return Ok(new { success = true, message = "دسترسی با موفقیت ویرایش شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("GetPermissionsByRoleId/{roleId}")]
        public async Task<IActionResult> GetPermissionsByRoleId(int roleId)
        {
            var permissions = await _permissionService.GetPermissionsByRole(roleId);
            return Ok(permissions);
        }

        // تخصیص دسترسی به نقش
        [HttpPost("AssignPermissions")]
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] AssignPermissionsDto dto)
        {
            try
            {

                await _permissionService.AssignPermissionsToRoleAsync(dto.RoleId, dto.PermissionIds);
                //await _roleService.AssignPermissionsToRoleAsync(dto.RoleId, dto.PermissionIds);
                return Ok(new { success = true, message = "دسترسی‌ها با موفقیت تخصیص داده شدند" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
