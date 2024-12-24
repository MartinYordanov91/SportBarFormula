using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.ErrorMessageConstants.DataErrorMessages;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Repository class for managing Reservation entities in the database.
/// </summary>
public class ReservationRepository(
    SportBarFormulaDbContext context
    ) : IRepository<Reservation>
{
    private readonly SportBarFormulaDbContext _context = context;

    /// <summary>
    /// Asynchronously adds a new reservation to the database.
    /// </summary>
    /// <param name="entity">The reservation to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Reservation entity is null.</exception>
    public async Task AddAsync(Reservation entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), ReservationObjectIsNull);
        }

        await _context.Reservations.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all reservations from the database.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation, containing an IEnumerable of reservations.</returns>
    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a reservation by its ID from the database.
    /// </summary>
    /// <param name="id">The ID of the reservation to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation, containing the reservation if found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the reservation is not found.</exception>
    public async Task<Reservation> GetByIdAsync(int id)
    {
        var entity = await _context.Reservations.FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException(ReservationNotFound);
        }

        return entity;
    }

    /// <summary>
    /// Asynchronously updates an existing reservation in the database.
    /// </summary>
    /// <param name="entity">The reservation to update.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the Reservation entity is null.</exception>
    public async Task UpdateAsync(Reservation entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), ReservationObjectIsNull);
        }

        _context.Reservations.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes a reservation from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the reservation to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the reservation is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Reservations.FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException(ReservationNotFound);
        }

        _context.Reservations.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
