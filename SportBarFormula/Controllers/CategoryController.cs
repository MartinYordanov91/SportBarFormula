using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Category;

namespace SportBarFormula.Controllers;

/// <summary>
/// Manage categories where you can add, remove and edit categories for items.
/// </summary>
/// <param name="categoryService"></param>
/// <param name="logger"></param>
[Authorize]
public class CategoryController(
    ICategoryService categoryService,
    IModelStateLoggerService logger) : Controller
{

    private readonly ICategoryService _service = categoryService;
    private readonly IModelStateLoggerService _logger = logger;

    //----------------------------------------------------------------------------------------------------> Index
    /// <summary>
    /// Retrieves and displays all categories.
    /// </summary>
    /// <returns>View with all categories.</returns>
    [HttpGet]
    [Authorize(Roles ="Admin,Manager")]
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
    [Authorize(Roles = "Admin,Manager")]
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
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _service.AddCategoryAsync(model);

        return RedirectToAction(nameof(Index));
    }

    //----------------------------------------------------------------------------------------------------> Edit
    /// <summary>
    /// Displays a form to edit an existing category by given id.
    /// </summary>
    /// <param name="id">Id of the category to edit.</param>
    /// <returns>View to edit the category if the category exists, or NotFound if it does not.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _service.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    /// <summary>
    /// Handles the post request to edit an existing category. 
    /// </summary>
    /// <param name="model">ViewModel with the updated category data.</param>
    /// <returns>Redirects to the Index view on successful update or displays the form again on failure</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _service.UpdateCategoryAsync(model);

        return RedirectToAction(nameof(Index));
    }

    //----------------------------------------------------------------------------------------------------> Delete
    /// <summary>
    /// Handles the post request to delete an existing category by a given ID.
    /// </summary>
    /// <param name="id">Identifier of the category to delete.</param>
    /// <returns>Redirects to the Index view on successful deletion or NotFound if the category does not exist.</returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _service.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        var result = await _service.DeleteCategoryAsync(id);

        if (!result)
        {
            TempData["ErrorMessage"] = "Не можете да изтриете тази категория, защото се използва.";
        }

        return RedirectToAction(nameof(Index));
    }

}
