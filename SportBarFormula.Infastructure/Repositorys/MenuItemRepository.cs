using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys;

public class MenuItemRepository(SportBarFormulaDbContext context) : IRepository<MenuItem>
{
    private readonly SportBarFormulaDbContext _context = context;

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems.Include(mi => mi.Category).ToListAsync();
    }


    public async Task<MenuItem> GetByIdAsync(int id)
    {
        return await _context.MenuItems.FindAsync(id);
    }

    public async Task AddAsync(MenuItem entity)
    {
        await _context.MenuItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MenuItem entity)
    {
        _context.MenuItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var menuitem = await _context.MenuItems.FindAsync(id);

        if (menuitem != null)
        {
            _context.MenuItems.Remove(menuitem);
            await _context.SaveChangesAsync();
        }
    }
}
