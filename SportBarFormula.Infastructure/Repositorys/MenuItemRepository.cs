using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.ErrorMessageConstants.DataErrorMessages;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Repository class for managing MenuItem entities in the database.
/// </summary>
public class MenuItemRepository(
    SportBarFormulaDbContext context
    ) : IRepository<MenuItem>
{
    private readonly SportBarFormulaDbContext _context = context;

    /// <summary>
    /// Adds a new menu item to the database.
    /// </summary>
    /// <param name="entity">The menu item to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Throws an exception if the MenuItem entity is null.</exception>
    public async Task AddAsync(MenuItem entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), MenuItemObjectIsNull);
        }

        await _context.MenuItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all menu items from the database.
    /// </summary>
    /// <returns>A list of all menu items, including their categories.</returns>
    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems
            .Include(mi => mi.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a menu item by its ID.
    /// </summary>
    /// <param name="id">The ID of the menu item.</param>
    /// <returns>The menu item with the specified ID, including its category.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the MenuItem is not found.</exception>
    public async Task<MenuItem> GetByIdAsync(int id)
    {
        var menuItem = await _context.MenuItems
            .Include(mi => mi.Category)
            .FirstOrDefaultAsync(mi => mi.MenuItemId == id);

        if (menuItem == null)
        {
            throw new KeyNotFoundException(MenuItemNotFound);
        }

        return menuItem;
    }

    /// <summary>
    /// Updates an existing menu item in the database.
    /// </summary>
    /// <param name="entity">The menu item to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Throws an exception if the MenuItem entity is null.</exception>
    public async Task UpdateAsync(MenuItem entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), MenuItemObjectIsNull);
        }
        _context.MenuItems.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a menu item by its ID.
    /// </summary>
    /// <param name="id">The ID of the menu item to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the MenuItem is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
        {
            throw new KeyNotFoundException(MenuItemNotFound);
        }

        menuItem.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
