using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Abstraction.Services;

namespace SchoolManagement.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _roleService.GetAllRoles();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetRoleById(string id)
    {
        var response = await _roleService.GetRoleById(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var response = await _roleService.CreateRole(roleName);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(string id, string roleName)
    {
        var response = await _roleService.UpdateRole(id, roleName);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var response = await _roleService.DeleteRole(id);
        return StatusCode(response.StatusCode, response);
    }
}