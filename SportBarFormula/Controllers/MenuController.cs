using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;

namespace SportBarFormula.Controllers
{
    public class MenuController(
        IMenuItemService service,
        ICategoryService categoryService
        ) : Controller
    {
        private readonly IMenuItemService _service = service;
        private readonly ICategoryService _categoryService = categoryService;

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
    }
}
