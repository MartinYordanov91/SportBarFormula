using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Category;

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

    //----------------------------------------------------------------------------------------------------> Index
    /// <summary>
    /// Retrieves and displays all categories.
    /// </summary>
    /// <returns>View with all categories.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _service.GetAllCategoriesAsync();

        return View(categories);
    }

    //----------------------------------------------------------------------------------------------------> Create
    /// <summary>
    /// Displays a form to create a new category.
    /// </summary>
    /// <returns>View to create a new category.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Processes the post request to create a new category.
    /// </summary>
    /// <param name="model">ViewModel for the new category.</param>
    /// <returns>Redirects to the Index view on successful creation or displays the form again on failure.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _service.AddCategoryAsync(model);

        return RedirectToAction(nameof(Index));
    }


}
