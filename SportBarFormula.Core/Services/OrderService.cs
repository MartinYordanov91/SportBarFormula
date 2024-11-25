using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using static SportBarFormula.Infrastructure.Constants.DataConstants.OrderConstants;

namespace SportBarFormula.Core.Services;

/// <summary>
/// Order Management Service.
/// </summary>
public class OrderService(IRepository<Order> repository) : IOrderService
{

    private readonly IRepository<Order> _repository = repository;

    /// <summary>
    /// Asynchronously retrieves an order by its ID from the repository and maps it to an OrderViewModel.
    /// </summary>
    /// <param name="id">The ID of the order to retrieve.</param>
    /// <returns>
    /// A Task representing the asynchronous operation, containing an OrderViewModel
    /// if the order is found, or null if the order is not found.
    /// </returns>
    public async Task<OrderViewModel?> GetOrderByIdAsync(int id)
    {
        var order = await _repository.GetByIdAsync(id);

        if (order == null)
        {
            return null;
        }

        var orderViewModel = new OrderViewModel()
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = order.OrderDate.ToString(OrderDateStringFormat),
            OrderItems = order.OrderItems
             .Select(oi => new OrderItemViewModel()
             {
                 MenuItemId = oi.MenuItemId,
                 OrderItemId = oi.OrderItemId,
                 MenuItemName = oi.MenuItem.Name,
                 Quantity = oi.Quantity,
                 Price = oi.Price,
             })
             .ToList(),
        };

        orderViewModel.TotalAmount = orderViewModel.OrderItems.Sum(item => item.Price * item.Quantity);

        return orderViewModel;
    }

}
