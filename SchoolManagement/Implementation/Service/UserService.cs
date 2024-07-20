using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.DTO.UserDto;
using SchoolManagement.Entities.Identity;
using SchoolManagement.Models;

namespace SchoolManagement.Implementation.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var response = new GenericResponseModel<bool>();

            try
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);

                response.Data = true;
                response.StatusCode = 200;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.InnerException);
                throw;
            }

            return response;
        }

        public async Task<GenericResponseModel<CreateUserResponseDto>> CreateAsync(CreateUserDto createUserDto)
        {
            var response = new GenericResponseModel<CreateUserResponseDto>();
            try
            {
                var id = Guid.NewGuid().ToString();
                
                if (createUserDto == null)
                {
                    response.StatusCode = 404;
                    response.Data = null;
                }

                var mappedUser = _mapper.Map<User>(createUserDto);
                mappedUser.Id = id;
                var result = await _userManager.CreateAsync(mappedUser, createUserDto.Password);
                response.StatusCode = 200;
                response.Data = new CreateUserResponseDto { Succeded = result.Succeeded };
                
                if (!result.Succeeded)
                {
                    response.StatusCode = 400;
                    response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
                }
                var user = (await _userManager.FindByNameAsync(createUserDto.Username) ?? await _userManager.FindByEmailAsync(createUserDto.Email)) ??
                            await _userManager.FindByIdAsync(id);
                
                if (user != null)
                    await _userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return response;
        }

        public async Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName)
        {
            var response = new GenericResponseModel<bool>();

            try
            {
                var user = await _userManager.FindByIdAsync(userIdOrName);

                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(userIdOrName);
                }

                if (user == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    return response;
                }

                IdentityResult result =  await _userManager.DeleteAsync(user);
                
                if (result.Succeeded)
                {
                    response.Data = true;
                    response.StatusCode = 201;
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return response;
        }

        public async Task<GenericResponseModel<List<UserGetDto>>> GetAllUsersAsync()
        {
            var response = new GenericResponseModel<List<UserGetDto>>();

            try
            {
                var users = await _userManager.Users.ToListAsync();

                var mappedUsers = _mapper.Map<List<UserGetDto>>(users);

                response.Data = mappedUsers;
                response.StatusCode = 200;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return response;
        }

        public async Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            var response = new GenericResponseModel<string[]>();

            var user = await _userManager.FindByIdAsync(userIdOrName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userIdOrName);
            }

            try
            {
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    response.Data = userRoles.ToArray();
                    response.StatusCode = 200;
                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.InnerException);
                throw;
            }

            return response;
        }

        public async Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var response = new GenericResponseModel<bool>();

            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userUpdateDto.UserId);

                if (user == null)
                {
                    response.Data = false;
                    response.StatusCode = 404;
                    return response;
                }

                await _userManager.UpdateAsync(user);
                response.Data = true;
                response.StatusCode = 204;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return response;
        }

        public async Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenDate.AddMinutes(5);
                await _userManager.UpdateAsync(user);
            }
        }
    }
}