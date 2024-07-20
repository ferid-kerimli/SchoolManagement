using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.DTO.UserDto;

namespace SchoolManagement.Controllers;

[Route("api/[controller][action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var response = await _userService.GetAllUsersAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesToUser(string userIdOrName)
    {
        var response = await _userService.GetRolesToUserAsync(userIdOrName);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
    {
        var response = await _userService.CreateAsync(createUserDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoleToUser(string userId, string[] roles)
    {
        var response = await _userService.AssignRoleToUserAsync(userId, roles);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        var response = await _userService.UpdateUserAsync(userUpdateDto);
        return StatusCode(response.StatusCode, response);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string username)
    {
        var response = await _userService.DeleteUserAsync(username);
        return StatusCode(response.StatusCode, response);
    }
}