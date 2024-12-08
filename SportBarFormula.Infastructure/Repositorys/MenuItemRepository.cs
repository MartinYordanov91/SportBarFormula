using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys
{
    public class MenuItemRepository(
        SportBarFormulaDbContext context
        ) : IRepository<MenuItem>
    {
        private readonly SportBarFormulaDbContext _context = context;

        /// <summary>
        /// Retrieves all menu items from the database.
        /// </summary>
        /// <returns>A list of all menu items, including their categories.</returns>
        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.Include(mi => mi.Category).ToListAsync();
        }

        /// <summary>
        /// Retrieves a menu item by its ID.
        /// </summary>
        /// <param name="id">The ID of the menu item.</param>
        /// <returns>The menu item with the specified ID, including its category.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the menu item is not found.</exception>
        public async Task<MenuItem> GetByIdAsync(int id)
        {
            var menuItem = await _context.MenuItems.Include(mi => mi.Category).FirstOrDefaultAsync(mi => mi.MenuItemId == id);

            if (menuItem == null)
            {
                throw new KeyNotFoundException("MenuItem not found");
            }

            return menuItem;
        }

        /// <summary>
        /// Adds a new menu item to the database.
        /// </summary>
        /// <param name="entity">The menu item to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(MenuItem entity)
        {
            await _context.MenuItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing menu item in the database.
        /// </summary>
        /// <param name="entity">The menu item to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(MenuItem entity)
        {
            _context.MenuItems.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a menu item by its ID.
        /// </summary>
        /// <param name="id">The ID of the menu item to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the menu item is not found.</exception>
        public async Task DeleteAsync(int id)
        {
            var menuitem = await _context.MenuItems.FindAsync(id);

            if (menuitem == null)
            {
                throw new KeyNotFoundException("MenuItem not found");
            }

            _context.MenuItems.Remove(menuitem);
            await _context.SaveChangesAsync();
        }
    }
}
