using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportBarFormula.Areas.Admin.Controllers;

/// <summary>
/// Controller for handling the home page in the Admin area.
/// </summary>
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    /// <summary>
    /// Displays the home page.
    /// </summary>
    /// <returns>A view for the home page.</returns>
    public IActionResult Index()
    {
        return View();
    }
}
