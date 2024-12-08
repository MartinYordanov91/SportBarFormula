using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys;

public class CategoryRepository(
    SportBarFormulaDbContext context
    ) : IRepository<Category>
{
    private readonly SportBarFormulaDbContext _context = context;

    /// <summary>
    /// Retrieves all categories from the database.
    /// </summary>
    /// <returns>A list of all categories.</returns>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The category with the specified ID.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the category is not found.</exception>
    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context.Categories.Include(c => c.MenuItems).FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        return category;
    }

    /// <summary>
    /// Adds a new category to the database.
    /// </summary>
    /// <param name="entity">The category to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing category in the database.
    /// </summary>
    /// <param name="entity">The category to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the category is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException("Category not found");
        }

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

