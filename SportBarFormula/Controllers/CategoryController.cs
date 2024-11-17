using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;

namespace SportBarFormula.Controllers;

/// <summary>
/// Manage categories where you can add, remove and edit categories for items.
/// </summary>
/// <param name="categoryService"></param>
/// <param name="logger"></param>
public class CategoryController(
    ICategoryService categoryService,
    ILogger<CategoryController> logger) : Controller
{

    private readonly ICategoryService _service = categoryService;
    private readonly ILogger<CategoryController> _logger = logger;


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAllCategoriesAsync();

        return View(categories);
    }


}
