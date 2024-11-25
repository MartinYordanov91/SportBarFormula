using Microsoft.AspNetCore.Http.HttpResults;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using System.Globalization;
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

    /// <summary>
    /// Asynchronously retrieves all orders from the repository and maps them to a list of OrderViewModel.
    /// </summary>
    /// <returns>
    /// A Task representing the asynchronous operation, containing an IEnumerable of OrderViewModel
    /// with the total amounts calculated for each order.
    /// </returns>
    public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
    {
        var allOrder = await _repository.GetAllAsync();

        var allOrderViewModel = allOrder
            .Select(o => new OrderViewModel()
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                OrderDate = o.OrderDate.ToString(OrderDateStringFormat),
                OrderItems = o.OrderItems
                    .Select(oi => new OrderItemViewModel()
                    {
                        MenuItemId = oi.MenuItemId,
                        OrderItemId = oi.OrderItemId,
                        MenuItemName = oi.MenuItem.Name,
                        Quantity = oi.Quantity,
                        Price = oi.Price,
                    })
                    .ToList()
            })
            .ToList();

        foreach (var order in allOrderViewModel)
        {
            order.TotalAmount = order.OrderItems.Sum(item => item.Price * item.Quantity);
        }

        return allOrderViewModel;
    }

    /// <summary>
    /// Updates an existing order with new information provided in the OrderViewModel.
    /// </summary>
    /// <param name="orderViewModel">The OrderViewModel containing updated order information.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">
    /// Thrown when the order is not found or the date format is not valid.
    /// </exception>
    public async Task UpdateOrderAsync(OrderViewModel orderViewModel)
    {
        var orderToUpdate = await _repository.GetByIdAsync(orderViewModel.OrderId);

        if (orderToUpdate == null)
        {
            throw new Exception("Order Not Found");
        }

        var isValid = DateTime.TryParseExact(orderViewModel.OrderDate, OrderDateStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime orderdata);

        if (!isValid)
        {
            throw new Exception("Data Format is not Valid");
        }

        var order = new Order()
        {
            OrderId = orderViewModel.OrderId,
            UserId = orderViewModel.UserId,
            OrderDate = orderdata,
            TotalAmount = orderViewModel.TotalAmount,
            OrderItems = orderViewModel.OrderItems
            .Select(oi => new OrderItem()
            {
                OrderId = orderViewModel.OrderId,
                OrderItemId = oi.OrderItemId,
                MenuItemId = oi.MenuItemId,
                Quantity = oi.Quantity,
                Price = oi.Price
            })
            .ToList()
        };

        orderToUpdate.OrderDate = order.OrderDate;
        orderToUpdate.TotalAmount = order.TotalAmount;
        orderToUpdate.OrderItems = order.OrderItems;

        await _repository.UpdateAsync(orderToUpdate);
    }

}
