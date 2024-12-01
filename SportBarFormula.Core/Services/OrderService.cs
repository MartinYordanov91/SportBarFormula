using Microsoft.AspNetCore.Identity;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

/// <summary>
/// Order Management Service.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<MenuItem> _menuitemRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    /// <param name="repository">The repository for orders.</param>
    /// <param name="orderItemRepository">The repository for order items.</param>
    /// <param name="menuitemRepository">The repository for menu items.</param>
    public OrderService(
        IRepository<Order> repository,
        IRepository<OrderItem> orderItemRepository,
        IRepository<MenuItem> menuitemRepository)
    {
        _repository = repository;
        _orderItemRepository = orderItemRepository;
        _menuitemRepository = menuitemRepository;
    }

    /// <summary>
    /// Adds an item to the cart.
    /// </summary>
    /// <param name="menuItemId">The ID of the menu item.</param>
    /// <param name="quantity">The quantity of the item.</param>
    /// <param name="order">The current order.</param>
    public async Task AddItemToCartAsync(int menuItemId, int quantity, OrderViewModel order)
    {
        var existOrderItem = order.OrderItems.FirstOrDefault(oi => oi.MenuItemId == menuItemId);

        if (existOrderItem == null)
        {
            var menuitem = await _menuitemRepository.GetByIdAsync(menuItemId);

            if (menuitem == null)
            {
                throw new Exception("MenuItem not found");
            }

            existOrderItem = new OrderItemViewModel()
            {
                MenuItemId = menuItemId,
                Quantity = quantity,
                MenuItemName = menuitem.Name,
                Price = menuitem.Price,
            };

            await AddOrderItemAsync(existOrderItem, order.OrderId);

            order.OrderItems.Add(existOrderItem);
        }
        else
        {
            existOrderItem.Quantity += quantity;
        }
    }

    /// <summary>
    /// Adds a new order.
    /// </summary>
    /// <param name="order">The order to add.</param>
    public async Task AddOrderAsync(OrderViewModel order)
    {
        var newOrder = new Order()
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = DateTime.Now,
            OrderItems = order.OrderItems
                .Select(oi => new OrderItem()
                {
                    MenuItemId = oi.MenuItemId,
                    OrderId = order.OrderId,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    OrderItemId = oi.OrderItemId,
                })
               .ToList(),
            Status = order.Status,
            TotalAmount = order.TotalAmount,
        };

        await _repository.AddAsync(newOrder);
    }

    /// <summary>
    /// Adds an order item to an existing order.
    /// </summary>
    /// <param name="orderitem">The order item to add.</param>
    /// <param name="orderId">The ID of the order.</param>
    public async Task AddOrderItemAsync(OrderItemViewModel orderitem, int orderId)
    {
        if (orderitem == null)
        {
            throw new Exception("Some order item field is missed");
        }

        var orderItem = new OrderItem()
        {
            OrderItemId = orderitem.OrderItemId,
            OrderId = orderId,
            MenuItemId = orderitem.MenuItemId,
            Price = orderitem.Price,
            Quantity = orderitem.Quantity,
        };

        await _orderItemRepository.AddAsync(orderItem);
    }

    /// <summary>
    /// Completes the order process.
    /// </summary>
    /// <param name="order">The order to complete.</param>
    public async Task CompletedOrderAsync(OrderViewModel order)
    {
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        if (order.OrderItems.Count > 0)
        {
            var orderToUpdate = await _repository.GetByIdAsync(order.OrderId);
            orderToUpdate.Status = OrderStatus.Completed;
            await _repository.UpdateAsync(orderToUpdate);
        }
    }

    /// <summary>
    /// Retrieves the draft order for the specified user.
    /// </summary>
    /// <param name="user">The user whose draft order is to be retrieved.</param>
    /// <returns>The draft order of the user.</returns>
    public async Task<OrderViewModel> GetUserDraftOrder(IdentityUser user)
    {
        var allOrders = await _repository.GetAllAsync();

        var order = allOrders
            .Where(o => o.UserId == user.Id && o.Status == OrderStatus.Draft)
            .FirstOrDefault();

        if (order == null)
        {
            var newOrder = new OrderViewModel()
            {
                UserId = user.Id,
                Status = OrderStatus.Draft,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
            };

            await AddOrderAsync(newOrder);
            return newOrder;
        }

        var orderModel = new OrderViewModel()
        {
            OrderId = order.OrderId,
            UserId = order.UserId,
            OrderDate = DateTime.Now,
            Status = OrderStatus.Draft,
            OrderItems = order.OrderItems
                    .Select(oi => new OrderItemViewModel()
                    {
                        OrderItemId = oi.OrderItemId,
                        MenuItemId = oi.MenuItemId,
                        MenuItemName = oi.MenuItem.Name,
                        Price = oi.Price,
                        Quantity = oi.Quantity,
                    }).ToList()
        };

        orderModel.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);

        await UpdateOrderAsync(orderModel);

        return orderModel;
    }

    /// <summary>
    /// Removes an item from the cart asynchronously.
    /// </summary>
    /// <param name="orderItemId">The ID of the order item to remove.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Throws an exception if the order item is not found.</exception>
    public async Task RemoveItemFormCartAsync(int orderItemId)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);

        if (orderItem == null)
        {
            throw new Exception("Order item not found");
        }

        await _orderItemRepository.DeleteAsync(orderItemId);
    }

    /// <summary>
    /// Updates an order TotalAmount asynchronously.
    /// </summary>
    /// <param name="orderModel">The order model containing updated information.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Throws an exception if the order is not found.</exception>
    public async Task UpdateOrderAsync(OrderViewModel orderModel)
    {
        var order = await _repository.GetByIdAsync(orderModel.OrderId);

        if (order == null)
        {
            throw new Exception("Order not found");
        }

        order.TotalAmount = orderModel.TotalAmount;

        await _repository.UpdateAsync(order);
    }


    /// <summary>
    /// Updates the quantity of an order item asynchronously.
    /// </summary>
    /// <param name="orderItemId">The ID of the order item to update.</param>
    /// <param name="quantity">The new quantity for the order item.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Throws an exception if the order item is not found.</exception>
    public async Task UpdateQuantityAsync(int orderItemId, int quantity)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);

        if (orderItem == null)
        {
            throw new Exception("Order item not found");
        }

        orderItem.Quantity = quantity;

        await _orderItemRepository.UpdateAsync(orderItem);
    }

}
