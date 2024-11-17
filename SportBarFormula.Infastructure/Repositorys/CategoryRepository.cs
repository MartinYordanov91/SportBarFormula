using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys;

public class CategoryRepository(SportBarFormulaDbContext context) : IRepository<Category>
{
    private readonly SportBarFormulaDbContext _context = context;

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context.Categories.Include(c => c.MenuItems).FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            throw new Exception("Category not Found");
        }

        return category;
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);

        if (entity != null)
        {
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        };
    }

}
