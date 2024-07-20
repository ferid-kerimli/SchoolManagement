using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Abstraction.Services;
using SchoolManagement.Entities.Identity;
using SchoolManagement.Models;

namespace SchoolManagement.Implementation.Service;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

public RoleService(RoleManager<Role> roleManager)
{
    _roleManager = roleManager;
}
public async Task<GenericResponseModel<bool>> CreateRole(string name)
{
    var response = new GenericResponseModel<bool>();
    try
    {
        if (string.IsNullOrEmpty(name))
        {
            response.Data = false;
            response.StatusCode = 404;
            return response;
        }
        IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
        if (result.Succeeded)
        {
            response.Data = result.Succeeded;
            response.StatusCode = 201;
            return response;
        }
        else
        {
            response.Data = result.Succeeded;
            response.StatusCode = 400;
            return response;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    return response;
}

public async Task<GenericResponseModel<bool>> DeleteRole(string id)
{
    var response = new GenericResponseModel<bool>();
    try
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            response.Data = false;
            response.StatusCode = 404;
            return response;
        }

        IdentityResult result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            response.Data = true;
            response.StatusCode = 200;
        }
        else
        {
            response.Data = false;
            response.StatusCode = 400;
        }
    }
    catch (Exception ex)
    {
        response.Data = false;
        response.StatusCode = 500;
        Console.WriteLine(ex.ToString());
    }
    return response;
}

public async Task<GenericResponseModel<object>> GetAllRoles()
{
    var response = new GenericResponseModel<object>();
    try
    {
        var roles = await _roleManager.Roles.ToListAsync();
        if (roles == null || !roles.Any())
        {
            response.Data = null;
            response.StatusCode = 404;
            return response;
        }
        else
        {
            response.Data = roles;
            response.StatusCode = 200;
        }
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.StatusCode = 500;
        Console.WriteLine(ex.ToString());
    }
    return response;
}

public async Task<GenericResponseModel<object>> GetRoleById(string id)
{
    var response = new GenericResponseModel<object>();
    try
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            response.Data = null;
            response.StatusCode = 404;
        }
        else
        {
            response.Data = role;
            response.StatusCode = 200;
        }
    }
    catch (Exception ex)
    {
        response.Data = null;
        response.StatusCode = 500;
        Console.WriteLine(ex.ToString());
    }
    return response;
}

public async Task<GenericResponseModel<bool>> UpdateRole(string id, string name)
{
    var response = new GenericResponseModel<bool>();
    try
    {
        if (string.IsNullOrEmpty(name))
        {
            response.Data = false;
            response.StatusCode = 400;
            return response;
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            response.Data = false;
            response.StatusCode = 404;
            return response;
        }

        role.Name = name;
        IdentityResult result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            response.Data = true;
            response.StatusCode = 200;
        }
        else
        {
            response.Data = false;
            response.StatusCode = 400;
        }
    }
    catch (Exception ex)
    {
        response.Data = false;
        response.StatusCode = 500;
        Console.WriteLine(ex.ToString());
    }
    return response;
}
}