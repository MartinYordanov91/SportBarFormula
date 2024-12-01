using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Infrastructure.Repositorys
{
    /// <summary>
    /// Repository for managing OrderItem entities.
    /// </summary>
    public class OrderItemRepository(SportBarFormulaDbContext context) : IRepository<OrderItem>
    {
        private readonly SportBarFormulaDbContext _context = context;

        /// <summary>
        /// Adds a new OrderItem entity to the database.
        /// </summary>
        /// <param name="entity">The OrderItem entity to add.</param>
        public async Task AddAsync(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an OrderItem entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the OrderItem to delete.</param>
        /// <exception cref="Exception">Throws an exception if the OrderItem is not found.</exception>
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.OrderItems.FindAsync(id);

            if (entity != null)
            {
                _context.OrderItems.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("OrderItem not found");
            }
        }

        /// <summary>
        /// Retrieves all OrderItem entities from the database.
        /// </summary>
        /// <returns>A list of OrderItem entities.</returns>
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        /// <summary>
        /// Retrieves an OrderItem entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the OrderItem to retrieve.</param>
        /// <returns>The OrderItem entity.</returns>
        /// <exception cref="Exception">Throws an exception if the OrderItem is not found.</exception>
        public async Task<OrderItem> GetByIdAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                throw new Exception("OrderItem not found");
            }

            return orderItem;
        }

        /// <summary>
        /// Updates an existing OrderItem entity in the database.
        /// </summary>
        /// <param name="entity">The OrderItem entity to update.</param>
        /// <exception cref="Exception">Throws an exception if the OrderItem entity is null.</exception>
        public async Task UpdateAsync(OrderItem entity)
        {
            if (entity == null)
            {
                throw new Exception("OrderItem is Null");
            }

            _context.OrderItems.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
