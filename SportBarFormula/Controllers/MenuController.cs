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
    /// Displays a list of all menu items.
    /// </summary>
    /// <param name="queryModel">The query model containing filter, search, and sorting options.</param>
    /// <returns>Returns the view with the filtered, sorted, and paginated list of menu items.</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index([FromQuery] AllMenuItemsQueryModel queryModel)
    {
        var serviceModel = await _service.AllAsync(queryModel);

        queryModel.Menuitems = serviceModel.MenuItems;
        queryModel.TotalMenuItems = serviceModel.TotalMenuItemsCount;

        var allcategory = await _categoryService.GetAllCategoriesAsync();
        queryModel.Categories = allcategory.Select(c => c.Name).ToList();

        return View(queryModel);
    }

  
   

    //--------------------------------------------------------------------------------------------------------------------->   Details
    /// <summary>
    /// Shows details about a specific menu item.
    /// </summary>
    /// <param name="queryModel">The query model containing filter and sorting options.</param>
    /// <param name="id">The ID of the menu item.</param>
    /// <returns>Returns the view with the menu item details.</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details([FromQuery] AllMenuItemsQueryModel queryModel, int id)
    {
        MenuItemDetailsViewModel viewModel = await _service.GetMenuItemDetailsByIdAsync(id);

        if (viewModel == null)
        {
            return NotFound();
        }

        ViewBag.MenuItemsPerPage = queryModel.MenuItemsPerPage;
        ViewBag.Category = queryModel.Category;
        ViewBag.SerchString = queryModel.SerchString;
        ViewBag.MenuItemSorting = queryModel.MenuItemSorting;
        ViewBag.CurrentPage = queryModel.CurrentPage;

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

        try
        {
            await _service.AddMenuItemAsync(model);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException)
        {
            ModelState.AddModelError(string.Empty, "There was an error creating the menu item.");
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "CategoryId", "Name");
            return View(model);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
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