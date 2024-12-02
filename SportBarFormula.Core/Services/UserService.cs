using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Admin;

namespace SportBarFormula.Core.Services;

/// <summary>
/// Service for managing users and their roles.
/// </summary>
public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the UserService class.
    /// </summary>
    /// <param name="userManager">UserManager instance for managing users.</param>
    /// <param name="roleManager">RoleManager instance for managing roles.</param>
    public UserService(UserManager<IdentityUser> userManager,
                       RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Gets all users asynchronously.
    /// </summary>
    /// <returns>A collection of IdentityUser objects.</returns>
    public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    /// <summary>
    /// Gets the ManageRolesViewModel for a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A ManageRolesViewModel object.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the user is not found.</exception>
    public async Task<ManageRolesViewModel> GetManageRolesViewModelAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();

        var model = new ManageRolesViewModel
        {
            UserId = user.Id,
            UserName = user.UserName!,
            UserRoles = userRoles,
            AllRoles = allRoles
        };

        return model;
    }

    /// <summary>
    /// Sets or unsets roles for a user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="roles">A list of roles to be set or unset for the user.</param>
    /// <exception cref="InvalidOperationException">Thrown when the user is not found.</exception>
    public async Task SetUnsetRolesToUsers(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();

        var addRoles = roles.Except(userRoles);
        var removeRoles = userRoles.Except(roles);

        await _userManager.AddToRolesAsync(user, addRoles);
        await _userManager.RemoveFromRolesAsync(user, removeRoles);
    }
}
