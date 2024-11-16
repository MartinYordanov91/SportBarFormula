using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.MenuItem;
using X.PagedList.Extensions;

namespace SportBarFormula.Controllers;

/// <summary>
/// Manages menu items and categories
/// </summary>
/// <param name="service"></param>
/// <param name="categoryService"></param>
/// <param name="logger"></param>
public class MenuController(
    IMenuItemService service,
    ICategoryService categoryService,
    ILogger<MenuController> logger
    ) : Controller
{
    private readonly IMenuItemService _service = service;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ILogger<MenuController> _logger = logger;

    /// <summary>
    ///  Displays lists of all menu items.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index(int? categoryId, int? page)
    {
        var menuItems = await _service.GetMenuItemsByCategoryAsync(categoryId);
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
        ViewBag.SelectedCategoryId = categoryId;

        int pageSize = 20;
        int pageNumber = (page ?? 1);

        var pagedList = menuItems.ToPagedList(pageNumber, pageSize);

        return View(pagedList);
    }


    /// <summary>
    /// Shows details about a specific menu item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var model = await _service.GetMenuItemByIdAsync(id);
        return View();
    }

    /// <summary>
    ///Form to create a new menu item.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
        return View();
    }

    /// <summary>
    /// Saves a new item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
            return View(model);
        }

        await _service.AddMenuItemAsync(model);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Form for editing an existing item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _service.GetMenuItemByIdAsync(id);
        return View(model);
    }
}
