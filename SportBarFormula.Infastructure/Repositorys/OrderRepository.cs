using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.ErrorMessageConstants.DataErrorMessages;

namespace SportBarFormula.Infrastructure.Repositorys;

/// <summary>
/// Repository class for managing Order entities in the database.
/// </summary>
public class OrderRepository(
    SportBarFormulaDbContext context
    ) : IRepository<Order>
{
    private readonly SportBarFormulaDbContext _context = context;


    /// <summary>
    /// Asynchronously adds a new order to the database.
    /// </summary>
    /// <param name="order">The order to add.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the order entity is null.</exception>
    public async Task AddAsync(Order entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), OrderObjectIsNull);
        }

        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously retrieves all orders from the database, including their associated order items and menu items.
    /// </summary>
    /// <returns>
    /// A Task representing the asynchronous operation, containing an IEnumerable of orders.
    /// Each order includes its associated order items, and each order item includes its associated menu item.
    /// </returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .ToListAsync();
    }


    /// <summary>
    /// Asynchronously retrieves an order by its ID from the database, including its associated order items and menu items.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>A Task representing the asynchronous operation, containing the order if found.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the order is not found.</exception>
    public async Task<Order> GetByIdAsync(int id)
    {
        var order = await _context
            .Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null)
        {
            throw new KeyNotFoundException(OrderNotFound);
        }

        return order;
    }

    /// <summary>
    /// Asynchronously updates an existing order in the database.
    /// </summary>
    /// <param name="entity">The order entity to update.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the order entity is null.</exception>
    public async Task UpdateAsync(Order entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), OrderObjectIsNull);
        }

        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Asynchronously deletes an order from the database by its ID.
    /// </summary>
    /// <param name="id">The ID of the order to delete.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the order is not found.</exception>
    public async Task DeleteAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            throw new KeyNotFoundException(OrderNotFound);
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
