﻿using SchoolManagement.Models;

namespace SchoolManagement.Abstraction.Services;

public interface IRoleService
{
    Task<GenericResponseModel<object>> GetAllRoles();
    Task<GenericResponseModel<object>> GetRoleById(string id);
    Task<GenericResponseModel<bool>> CreateRole(string name);
    Task<GenericResponseModel<bool>> DeleteRole(string id);
    Task<GenericResponseModel<bool>> UpdateRole(string id, string name);
}