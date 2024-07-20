using SchoolManagement.DTO.UserDto;
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