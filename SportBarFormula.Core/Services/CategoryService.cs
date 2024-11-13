using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data;

namespace SportBarFormula.Core.Services
{
    public class CategoryService(SportBarFormulaDbContext context) : ICategoryService
    {
        private readonly SportBarFormulaDbContext _context = context;
        public async Task<ICollection<CategoryViewModel>> GetAllCategoyAsinc()
        {
            return await _context.Categories
                 .Select(c => new CategoryViewModel
                 {
                     CategoryId = c.CategoryId,
                     Name = c.Name,
                 })
                 .ToListAsync();
        }
    }
}
