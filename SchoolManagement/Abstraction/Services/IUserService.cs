using SchoolManagement.Entities.Identity;
using SchoolManagement.Models;

namespace SchoolManagement.Abstraction.Services;

public interface IUserService
{
    Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate);
    public Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles);
    Task<GenericResponseModel<CreateUserResponseDto>> CreateAsync(CreateUserDto createUserDto);
    public Task<GenericResponseModel<List<UserGetDto>>> GetAllUsersAsync();
    public Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName);
    public Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName);
    public Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDto userUpdateDto);
}

public class CreateUserResponseDto
{
    public bool Succeded { get; set; }
    public string Message { get; set; }
}

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UserGetDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class UserUpdateDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
