using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SportBarFormula.Areas.Admin.Controllers;

/// <summary>
/// Controller for managing roles in the Admin area.
/// </summary>
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the RoleController class.
    /// </summary>
    /// <param name="roleManager">RoleManager instance for managing roles.</param>
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    /// <summary>
    /// Displays a list of all roles.
    /// </summary>
    /// <returns>A view with a list of roles.</returns>
    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }
}
