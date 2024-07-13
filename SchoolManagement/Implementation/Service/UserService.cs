using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Abstraction.Services;
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
        public Task<GenericResponseModel<bool>> AssignRoleToUserAsync(string userId, string[] roles)
        {
            throw new NotImplementedException();
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
                var result = await _userManager.CreateAsync(mappedUser, createUserDto.Password);
                response.StatusCode = 200;
                response.Data = new CreateUserResponseDto { Succeded = result.Succeeded };

                if (!result.Succeeded)
                {
                    response.StatusCode = 400;
                    response.Data.Message = string.Join(" \n ", result.Errors.Select(error => $"{error.Code} - {error.Description}"));
                }
                User user = await _userManager.FindByNameAsync(createUserDto.Username);
                if (user == null)
                    user = await _userManager.FindByEmailAsync(createUserDto.Email);
                if (user == null)
                    user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    await _userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public Task<GenericResponseModel<bool>> DeleteUserAsync(string userIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<UserGetDto>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<string[]>> GetRolesToUserAsync(string userIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshToken(string refreshToken, User user, DateTime accessTokenDate)
        {
            throw new NotImplementedException();
        }
    }
}