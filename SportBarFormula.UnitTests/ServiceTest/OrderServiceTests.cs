using Microsoft.AspNetCore.Identity;
using SportBarFormula.Core.Services;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.ServiceTest;

[TestFixture]
public class OrderServiceTests
{
    private SportBarFormulaDbContext _dbContext;
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {

        _dbContext = MockDbContextFactory.Create();

        var orderRepository = new OrderRepository(_dbContext);
        var orderItemRepository = new OrderItemRepository(_dbContext);
        var menuItemRepository = new MenuItemRepository(_dbContext);

        _orderService = new OrderService(orderRepository, orderItemRepository, menuItemRepository);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests if AddItemToCartAsync adds a new item to the cart correctly.
    /// </summary>
    [Test]
    public async Task AddItemToCartAsync_ShouldAddNewItemToCart()
    {
        // Arrange
        var menuItemId = 1; // ID на съществуващ артикул от менюто
        var quantity = 2;
        var order = new OrderViewModel
        {
            OrderId = 1,
            UserId = "test-user-id",
            OrderItems = new List<OrderItemViewModel>()
        };

        // Act
        await _orderService.AddItemToCartAsync(menuItemId, quantity, order);

        // Assert
        var addedItem = order.OrderItems.FirstOrDefault(oi => oi.MenuItemId == menuItemId);
        Assert.That(addedItem, Is.Not.Null);
        var expectedMenuItem = _dbContext.MenuItems.Find(menuItemId);
        Assert.That(expectedMenuItem, Is.Not.Null);
        Assert.That(addedItem.MenuItemName, Is.EqualTo(expectedMenuItem.Name));
        Assert.That(addedItem.Price, Is.EqualTo(expectedMenuItem.Price));
        Assert.That(addedItem.Quantity, Is.EqualTo(quantity));
    }

    /// <summary>
    /// Tests if AddItemToCartAsync updates the quantity of an existing item in the cart.
    /// </summary>
    [Test]
    public async Task AddItemToCartAsync_ShouldUpdateQuantityOfExistingItemInCart()
    {
        // Arrange
        var menuItemId = 1; // ID на съществуващ артикул от менюто
        var initialQuantity = 1;
        var additionalQuantity = 2;
        var order = new OrderViewModel
        {
            OrderId = 1,
            UserId = "test-user-id",
            OrderItems = new List<OrderItemViewModel>
        {
            new OrderItemViewModel { OrderItemId = 1, MenuItemId = menuItemId, Quantity = initialQuantity }
        }
        };

        // Act
        await _orderService.AddItemToCartAsync(menuItemId, additionalQuantity, order);

        // Assert
        var updatedItem = _dbContext.OrderItems.FirstOrDefault(oi => oi.MenuItemId == menuItemId);
        Assert.That(updatedItem, Is.Not.Null);
        Assert.That(updatedItem.Quantity, Is.EqualTo(initialQuantity + additionalQuantity));
    }

    /// <summary>
    /// Tests if AddItemToCartAsync throws InvalidOperationException when menu item is not found.
    /// </summary>
    [Test]
    public void AddItemToCartAsync_ShouldThrowInvalidOperationException_WhenMenuItemNotFound()
    {
        // Arrange
        var nonExistingMenuItemId = 999; // ID, който не съществува
        var quantity = 2;
        var order = new OrderViewModel
        {
            OrderId = 1,
            UserId = "test-user-id",
            OrderItems = new List<OrderItemViewModel>()
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.AddItemToCartAsync(nonExistingMenuItemId, quantity, order));
        Assert.That(exception.Message, Is.EqualTo("No MenuItem found in the repository."));
    }

    /// <summary>
    /// Tests if AddOrderAsync adds a new order correctly.
    /// </summary>
    [Test]
    public async Task AddOrderAsync_ShouldAddNewOrder()
    {
        // Arrange
        var newOrder = new OrderViewModel
        {
            OrderId = 4, // Задаваме ID, което е ново и не съществува в базата данни
            UserId = "test-user-id",
            OrderDate = DateTime.UtcNow,
            OrderItems = new List<OrderItemViewModel>
        {
            new OrderItemViewModel { OrderItemId = 4, MenuItemId = 1, Quantity = 2, Price = 8.99m },
            new OrderItemViewModel { OrderItemId = 5, MenuItemId = 2, Quantity = 1, Price = 1.99m }
        },
            Status = OrderStatus.Draft,
            TotalAmount = 19.97m
        };

        // Act
        await _orderService.AddOrderAsync(newOrder);

        // Assert
        var addedOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderId == newOrder.OrderId);
        Assert.That(addedOrder, Is.Not.Null);
        Assert.That(addedOrder.UserId, Is.EqualTo(newOrder.UserId));
        Assert.That(addedOrder.Status, Is.EqualTo(newOrder.Status));
        Assert.That(addedOrder.TotalAmount, Is.EqualTo(newOrder.TotalAmount));
        Assert.That(addedOrder.OrderItems.Count, Is.EqualTo(newOrder.OrderItems.Count));

        foreach (var orderItem in newOrder.OrderItems)
        {
            var addedOrderItem = addedOrder.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItem.OrderItemId);
            Assert.That(addedOrderItem, Is.Not.Null);
            Assert.That(addedOrderItem.MenuItemId, Is.EqualTo(orderItem.MenuItemId));
            Assert.That(addedOrderItem.Quantity, Is.EqualTo(orderItem.Quantity));
            Assert.That(addedOrderItem.Price, Is.EqualTo(orderItem.Price));
        }
    }

    /// <summary>
    /// Tests if AddOrderItemAsync adds an order item to an existing order correctly.
    /// </summary>
    [Test]
    public async Task AddOrderItemAsync_ShouldAddOrderItemToExistingOrder()
    {
        // Arrange
        var orderId = 1; // ID на съществуваща поръчка
        var newOrderItem = new OrderItemViewModel
        {
            OrderItemId = 4, // Задаваме нов ID за артикула
            MenuItemId = 1, // ID на съществуващ артикул от менюто
            Quantity = 2,
            Price = 8.99m
        };

        // Act
        await _orderService.AddOrderItemAsync(newOrderItem, orderId);

        // Assert
        var addedOrderItem = _dbContext.OrderItems.FirstOrDefault(oi => oi.OrderItemId == newOrderItem.OrderItemId);
        Assert.That(addedOrderItem, Is.Not.Null);
        Assert.That(addedOrderItem.OrderId, Is.EqualTo(orderId));
        Assert.That(addedOrderItem.MenuItemId, Is.EqualTo(newOrderItem.MenuItemId));
        Assert.That(addedOrderItem.Quantity, Is.EqualTo(newOrderItem.Quantity));
        Assert.That(addedOrderItem.Price, Is.EqualTo(newOrderItem.Price));
    }

    /// <summary>
    /// Tests if AddOrderItemAsync throws an exception when order item is null.
    /// </summary>
    [Test]
    public void AddOrderItemAsync_ShouldThrowException_WhenOrderItemIsNull()
    {
        // Arrange
        var orderId = 1; // ID на съществуваща поръчка
        OrderItemViewModel nullOrderItem = null!;

        // Act & Assert
        var exception = Assert.ThrowsAsync<Exception>(async () => await _orderService.AddOrderItemAsync(nullOrderItem, orderId));
        Assert.That(exception.Message, Is.EqualTo("Some order item field is missed"));
    }

    /// <summary>
    /// Tests if CompletedOrderAsync completes the order correctly.
    /// </summary>
    [Test]
    public async Task CompletedOrderAsync_ShouldCompleteOrder()
    {
        // Arrange
        var order = new OrderViewModel
        {
            OrderId = 1,
            UserId = "test-user-id",
            OrderItems = new List<OrderItemViewModel>
        {
            new OrderItemViewModel { OrderItemId = 1, MenuItemId = 1, Quantity = 1, Price = 8.99m }
        },
            Status = OrderStatus.Draft
        };

        // Act
        await _orderService.CompletedOrderAsync(order);

        // Assert
        var completedOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        Assert.That(completedOrder, Is.Not.Null);
        Assert.That(completedOrder.Status, Is.EqualTo(OrderStatus.Completed));
    }

    /// <summary>
    /// Tests if CompletedOrderAsync throws ArgumentNullException when order is null.
    /// </summary>
    [Test]
    public void CompletedOrderAsync_ShouldThrowArgumentNullException_WhenOrderIsNull()
    {
        // Arrange
        OrderViewModel nullOrder = null!;

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _orderService.CompletedOrderAsync(nullOrder));
        Assert.That(exception.ParamName, Is.EqualTo("order"));
        Assert.That(exception.Message, Does.Contain("Order not found"));
    }

    /// <summary>
    /// Tests if GetUserDraftOrder returns the draft order for the specified user.
    /// </summary>
    [Test]
    public async Task GetUserDraftOrder_ShouldReturnDraftOrderForUser()
    {
        // Arrange
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == "user2");

        // Act
        Assert.That(user, Is.Not.Null);
        var result = await _orderService.GetUserDraftOrder(user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(user.Id));
        Assert.That(result.Status, Is.EqualTo(OrderStatus.Draft));
        Assert.That(result.OrderItems.Count, Is.EqualTo(_dbContext.Orders.FirstOrDefault(o => o.UserId == user.Id && o.Status == OrderStatus.Draft)?.OrderItems.Count ?? 0));
    }

    /// <summary>
    /// Tests if GetUserDraftOrder creates a new draft order if none exists for the user.
    /// </summary>
    [Test]
    public async Task GetUserDraftOrder_ShouldCreateNewDraftOrderIfNoneExists()
    {
        // Arrange
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == "user3");

        // Act
        Assert.That(user, Is.Not.Null);
        var result = await _orderService.GetUserDraftOrder(user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo(user.Id));
        Assert.That(result.Status, Is.EqualTo(OrderStatus.Draft));
        Assert.That(result.TotalAmount, Is.EqualTo(0));
        Assert.That(_dbContext.Orders.Any(o => o.UserId == user.Id && o.Status == OrderStatus.Draft), Is.True);
    }

    /// <summary>
    /// Tests if RemoveItemFormCartAsync removes an item from the cart correctly.
    /// </summary>
    [Test]
    public async Task RemoveItemFormCartAsync_ShouldRemoveItemFromCart()
    {
        // Arrange
        var orderItemId = 1; // ID на съществуващ артикул в поръчката

        // Act
        await _orderService.RemoveItemFormCartAsync(orderItemId);

        // Assert
        var removedItem = _dbContext.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
        Assert.That(removedItem, Is.Null);
    }

    /// <summary>
    /// Tests if RemoveItemFormCartAsync throws InvalidOperationException when order item is not found.
    /// </summary>
    [Test]
    public void RemoveItemFormCartAsync_ShouldThrowInvalidOperationException_WhenOrderItemNotFound()
    {
        // Arrange
        var nonExistingOrderItemId = 999; // ID, който не съществува

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.RemoveItemFormCartAsync(nonExistingOrderItemId));
        Assert.That(exception.Message, Is.EqualTo("No orderItem found in the repository."));
    }

    /// <summary>
    /// Tests if UpdateOrderAsync updates the total amount of an order correctly.
    /// </summary>
    [Test]
    public async Task UpdateOrderAsync_ShouldUpdateTotalAmount()
    {
        // Arrange
        var existingOrderId = 1;
        var updatedOrderModel = new OrderViewModel
        {
            OrderId = existingOrderId,
            TotalAmount = 25.00m // Нова стойност за общата сума
        };

        // Act
        await _orderService.UpdateOrderAsync(updatedOrderModel);

        // Assert
        var updatedOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderId == existingOrderId);
        Assert.That(updatedOrder, Is.Not.Null);
        Assert.That(updatedOrder.TotalAmount, Is.EqualTo(updatedOrderModel.TotalAmount));
    }

    /// <summary>
    /// Tests if UpdateOrderAsync throws InvalidOperationException when order is not found.
    /// </summary>
    [Test]
    public void UpdateOrderAsync_ShouldThrowInvalidOperationException_WhenOrderNotFound()
    {
        // Arrange
        var nonExistingOrderId = 999; // ID, който не съществува
        var updatedOrderModel = new OrderViewModel
        {
            OrderId = nonExistingOrderId,
            TotalAmount = 25.00m
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.UpdateOrderAsync(updatedOrderModel));
        Assert.That(exception.Message, Is.EqualTo("No order found in the repository."));
    }

    /// <summary>
    /// Tests if UpdateQuantityAsync updates the quantity of an order item correctly.
    /// </summary>
    [Test]
    public async Task UpdateQuantityAsync_ShouldUpdateQuantity()
    {
        // Arrange
        var existingOrderItemId = 1;
        var newQuantity = 5;

        // Act
        await _orderService.UpdateQuantityAsync(existingOrderItemId, newQuantity);

        // Assert
        var updatedOrderItem = _dbContext.OrderItems.FirstOrDefault(oi => oi.OrderItemId == existingOrderItemId);
        Assert.That(updatedOrderItem, Is.Not.Null);
        Assert.That(updatedOrderItem.Quantity, Is.EqualTo(newQuantity));
    }

    /// <summary>
    /// Tests if UpdateQuantityAsync throws InvalidOperationException when order item is not found.
    /// </summary>
    [Test]
    public void UpdateQuantityAsync_ShouldThrowInvalidOperationException_WhenOrderItemNotFound()
    {
        // Arrange
        var nonExistingOrderItemId = 999; // ID, който не съществува
        var newQuantity = 5;

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.UpdateQuantityAsync(nonExistingOrderItemId, newQuantity));
        Assert.That(exception.Message, Is.EqualTo("No orderItem found in the repository."));
    }

}
