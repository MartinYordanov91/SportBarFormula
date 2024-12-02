using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.MenuItem;
using X.PagedList.Extensions;

namespace SportBarFormula.Controllers;

/// <summary>
/// Manages menu items and categories
/// </summary>
/// <param name="service"></param>
/// <param name="categoryService"></param>
/// <param name="logger"></param>
[Authorize]
public class MenuController(
    IMenuItemService service,
    ICategoryService categoryService,
    IModelStateLoggerService logger
    ) : Controller
{
    private readonly IMenuItemService _service = service;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IModelStateLoggerService _logger = logger;

    //--------------------------------------------------------------------------------------------------------------------->   Index
    /// <summary>
    ///  Displays lists of all menu items.
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
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

    //--------------------------------------------------------------------------------------------------------------------->   Details
    /// <summary>
    /// Shows details about a specific menu item.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id, int? categoryId)
    {
        MenuItemDetailsViewModel viewModel = await _service.GetMenuItemDetailsByIdAsync(id);

        if (viewModel == null)
        {
            return NotFound();
        }

        ViewBag.SelectedCategoryId = categoryId;

        return View(viewModel);
    }

    //--------------------------------------------------------------------------------------------------------------------->   Create
    /// <summary>
    ///Form to create a new menu item.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
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
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
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

    //--------------------------------------------------------------------------------------------------------------------->   Edit
    /// <summary>
    /// Form for editing an existing item.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _service.GetMenuItemEditFormByIdAsync(id);
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name", model.CategoryId);

        return View(model);
    }

    /// <summary>
    /// Saves changes to the item.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MenuItemEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name", model.CategoryId);
            return View(model);
        }

        await _service.UpdateMenuItemAsync(model);
        return RedirectToAction(nameof(Details), new { id = model.MenuItemId });
    }

    //--------------------------------------------------------------------------------------------------------------------->   DeletedItems
    /// <summary>
    /// <summary>
    /// Shows all deleted items.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeletedItems(int? categoryId, int? page)
    {
        var deletedItems = await _service.GetDeletedItemsByCategoryAsync(categoryId);
        ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
        ViewBag.SelectedCategoryId = categoryId;

        int pageSize = 4;
        int pageNumber = (page ?? 1);

        var pagedList = deletedItems.ToPagedList(pageNumber, pageSize);

        return View(pagedList);
    }

    /// <summary>
    /// restores a deleted item and references item details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UnDelete(int id)
    {
        await _service.UnDeleteItemAsync(id);

        return RedirectToAction(nameof(Details), new { id });
    }

}