using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;

namespace SportBarFormula.Areas.Admin.Controllers;

/// <summary>
/// Controller for managing users and their roles in the Admin area.
/// </summary>
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the UsersController class.
    /// </summary>
    /// <param name="userService">UserService instance for managing users and roles.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    //--------------------------------------------------------------------------------------------------------------> Index
    /// <summary>
    /// Displays a list of all users.
    /// </summary>
    /// <returns>A view with a list of users.</returns>
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }

    //--------------------------------------------------------------------------------------------------------------> ManageRoles
    /// <summary>
    /// Displays the manage roles view for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A view for managing the user's roles.</returns>
    [HttpGet]
    public async Task<IActionResult> ManageRoles(string userId)
    {
        try
        {
            var model = await _userService.GetManageRolesViewModelAsync(userId);
            return View(model);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Updates the roles for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="roles">A list of roles to be assigned to the user.</param>
    /// <returns>A redirect to the Index action.</returns>
    [HttpPost]
    public async Task<IActionResult> ManageRoles(string userId, List<string> roles)
    {
        try
        {
            await _userService.SetUnsetRolesToUsers(userId, roles);
        }
        catch (Exception)
        {
            return NotFound();
        }

        return RedirectToAction("Index");
    }
}
