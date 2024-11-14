using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Controllers
{
    public class MenuController(
        IMenuItemService service,
        ICategoryService categoryService,
        ILogger<MenuController> logger
        ) : Controller
    {
        private readonly IMenuItemService _service = service;
        private readonly ICategoryService _categoryService = categoryService;
        private readonly ILogger<MenuController> _logger = logger;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await _service.GetAllMenuItemsAsync();
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _service.GetMenuItemByIdAsync(id);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoyAsinc(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoyAsinc(), "CategoryId", "Name");
                return View(model);
            }

            await _service.AddMenuItem(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
