using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.ViewModels;
using System.Diagnostics;

namespace SportBarFormula.Controllers;

/// <summary>
/// Home controller for handling main application pages.
/// </summary>
public class HomeController() : Controller
{
    /// <summary>
    /// Handles the Index page request.
    /// </summary>
    /// <returns>Returns the Index view.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Handles the Privacy page request.
    /// </summary>
    /// <returns>Returns the Privacy view.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Handles errors and returns the Error view.
    /// </summary>
    /// <returns>Returns the Error view with the error details.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
