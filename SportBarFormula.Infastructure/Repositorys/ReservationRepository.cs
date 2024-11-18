using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Reservation Management Repository.
/// </summary>
public class ReservationRepository(SportBarFormulaDbContext context) : IRepository<Reservation>
{
    private readonly SportBarFormulaDbContext _context = context;


    /// <summary>
    /// Adds a new reservation to the database.
    /// </summary>
    /// <param name="entity">The reservation to add.</param>
    public async Task AddAsync(Reservation entity)
    {
        await _context.Reservations.AddAsync(entity);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Deletes a reservation by a given ID.
    /// </summary>
    /// <param name="id">ID of the reservation to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Reservations.FindAsync(id);

        if (entity != null)
        {
            _context.Reservations.Remove(entity);
            await _context.SaveChangesAsync();
        };
    }


    /// <summary>
    /// Returns all reservations
    /// </summary>
    /// <returns>List of all reservations.</returns>
    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _context.Reservations.ToListAsync();
    }


    /// <summary>
    /// Returns a reservation by given id.
    /// </summary>
    /// <param name="id">The ID of the reservation.</param>
    /// <returns>The reservation or an exception if not found.</returns>
    public async Task<Reservation> GetByIdAsync(int id)
    {
        var entity = await _context.Reservations.FindAsync(id);

        if (entity == null)
        {
            throw new Exception("Reservation not found");
        }

        return entity;
    }


    /// <summary>
    /// Updates an existing reservation. 
    /// </summary>
    /// <param name="entity">The reservation to update.</param>
    public async Task UpdateAsync(Reservation entity)
    {
        _context.Reservations.Update(entity);
        await _context.SaveChangesAsync();
    }
}
