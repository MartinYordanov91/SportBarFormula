using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.ErrorMessageConstants.DataErrorMessages;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Repository class for managing Table entities in the database.
/// </summary>
public class TableRepository(
    SportBarFormulaDbContext context
    ) : IRepository<Table>
{
    private readonly SportBarFormulaDbContext _context = context;

    /// <summary>
    /// Gets all Table entities from the database asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with a collection of Table entities.</returns>
    public async Task<IEnumerable<Table>> GetAllAsync()
    {
        return await _context.Tables.ToListAsync();
    }

    /// <summary>
    /// Adds a new Table entity to the database asynchronously.
    /// </summary>
    /// <param name="entity">The Table entity to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Table entity is null.</exception>
    public async Task AddAsync(Table entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), TableObjectIsNull);
        }

        await _context.Tables.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a Table entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the Table entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, with the Table entity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the Table entity is not found.</exception>
    public async Task<Table> GetByIdAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
        {
            throw new KeyNotFoundException(TableNotFound);
        }

        return table;
    }

    /// <summary>
    /// Updates an existing Table entity in the database asynchronously.
    /// </summary>
    /// <param name="entity">The Table entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Table entity is null.</exception>
    public async Task UpdateAsync(Table entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), TableObjectIsNull);
        }

        _context.Tables.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a Table entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the Table entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the Table entity is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
        {
            throw new KeyNotFoundException(TableNotFound);
        }

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
    }
}
