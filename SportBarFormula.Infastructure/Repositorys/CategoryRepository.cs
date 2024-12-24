using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.ErrorMessageConstants.DataErrorMessages;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Repository class for managing Category entities in the database.
/// </summary>
public class CategoryRepository(
    SportBarFormulaDbContext context
    ) : IRepository<Category>
{
    private readonly SportBarFormulaDbContext _context = context;



    /// <summary>
    /// Adds a new Category to the database.
    /// </summary>
    /// <param name="entity">The Category to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Throws an exception if the Category entity is null.</exception>
    public async Task AddAsync(Category entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), CategoryObjectIsNull);
        }

        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all categories from the database including their MenuItems.
    /// </summary>
    /// <returns>A list of all categories.</returns>
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Include(c => c.MenuItems)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a Category by its ID including their MenuItems.
    /// </summary>
    /// <param name="id">The ID of the Category.</param>
    /// <returns>The Category with the specified ID.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the Category is not found.</exception>
    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .Include(c => c.MenuItems)
            .FirstOrDefaultAsync(c => c.CategoryId == id);

        if (category == null)
        {
            throw new KeyNotFoundException(CategoryNotFound);
        }

        return category;
    }

    /// <summary>
    /// Updates an existing Category in the database.
    /// </summary>
    /// <param name="entity">The Category to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Throws an exception if the Category entity is null.</exception>
    public async Task UpdateAsync(Category entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), CategoryObjectIsNull);
        }
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a Category by its ID.
    /// </summary>
    /// <param name="id">The ID of the Category to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the Category is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Categories.FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException(CategoryNotFound);
        }

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

