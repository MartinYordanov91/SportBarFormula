using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Repositorys;

[TestFixture]
public class OrderItemRepositoryTests
{
    private DbContextOptions<SportBarFormulaDbContext> _options;
    private SportBarFormulaDbContext _dbContext;
    private OrderItemRepository _orderItemRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _options = new DbContextOptionsBuilder<SportBarFormulaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new SportBarFormulaDbContext(_options);
        _orderItemRepository = new OrderItemRepository(_dbContext);

        // Seed data
        var user = new IdentityUser
        {
            Id = "test-user-id",
            UserName = "testuser",
            Email = "testuser@example.com"
        };

        var menuItem = new MenuItem
        {
            MenuItemId = 1,
            Name = "Burger",
            Price = 5.99m,
            Quantity = 1,
            ImageURL = "burger.jpg",
            PreparationTime = 10,
            CategoryId = 1
        };

        var order = new Order
        {
            OrderId = 1,
            UserId = user.Id,
            OrderDate = DateTime.Now,
            TotalAmount = 5.99m,
            Status = OrderStatus.Draft
        };

        var orderItem = new OrderItem
        {
            OrderItemId = 1,
            OrderId = order.OrderId,
            MenuItemId = menuItem.MenuItemId,
            Quantity = 1,
            Price = menuItem.Price
        };

        _dbContext.Users.Add(user);
        _dbContext.MenuItems.Add(menuItem);
        _dbContext.Orders.Add(order);
        _dbContext.OrderItems.Add(orderItem);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Cleans up the in-memory database after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests if AddAsync adds a new OrderItem and links it to the existing Order.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddOrderItem_AndLinkToOrder()
    {
        // Arrange
        var newOrderItem = new OrderItem
        {
            OrderItemId = 2,
            OrderId = 1, // Existing order
            MenuItemId = 1, // Existing menu item
            Quantity = 2,
            Price = 11.98m
        };

        // Act
        await _orderItemRepository.AddAsync(newOrderItem);

        // Assert
        var order = await _dbContext.Orders.Include(o => o.OrderItems).FirstAsync(o => o.OrderId == 1);
        Assert.That(order.OrderItems.Count, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests if DeleteAsync removes the OrderItem and preserves the Order.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveOrderItem_AndPreserveOrder()
    {
        // Act
        await _orderItemRepository.DeleteAsync(1);

        // Assert
        var order = await _dbContext.Orders.Include(o => o.OrderItems).FirstAsync(o => o.OrderId == 1);
        Assert.That(order.OrderItems.Count, Is.EqualTo(0));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns the OrderItem with correct details.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnOrderItem_WithCorrectDetails()
    {
        // Act
        var orderItem = await _orderItemRepository.GetByIdAsync(1);

        // Assert
        Assert.That(orderItem.OrderItemId, Is.EqualTo(1));
        Assert.That(orderItem.Quantity, Is.EqualTo(1));
        Assert.That(orderItem.Price, Is.EqualTo(5.99m));
    }

    /// <summary>
    /// Tests if UpdateAsync modifies the details of an existing OrderItem.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldModifyOrderItemDetails()
    {
        // Arrange
        var orderItem = await _orderItemRepository.GetByIdAsync(1);
        orderItem.Quantity = 3;
        orderItem.Price = 17.97m;

        // Act
        await _orderItemRepository.UpdateAsync(orderItem);

        // Assert
        var updatedOrderItem = await _dbContext.OrderItems.FindAsync(1);

        Assert.That(updatedOrderItem, Is.Not.Null);
        Assert.That(updatedOrderItem?.Quantity, Is.EqualTo(3));
        Assert.That(updatedOrderItem?.Price, Is.EqualTo(17.97m));
    }

    /// <summary>
    /// Tests if GetAllAsync returns all OrderItems.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllOrderItems()
    {
        // Act
        var orderItems = await _orderItemRepository.GetAllAsync();

        // Assert
        Assert.That(orderItems.Count(), Is.EqualTo(1));
    }

    /// <summary>
    /// Tests if the Order status remains Draft after adding an OrderItem.
    /// </summary>
    [Test]
    public async Task OrderStatus_ShouldRemainDraft_AfterAddingOrderItem()
    {
        // Arrange
        var order = await _dbContext.Orders.FindAsync(1);

        // Act
        var newOrderItem = new OrderItem
        {
            OrderItemId = 2,
            OrderId = 1,
            MenuItemId = 1,
            Quantity = 1,
            Price = 5.99m
        };
        await _orderItemRepository.AddAsync(newOrderItem);

        // Assert
        Assert.That(order, Is.Not.Null);
        Assert.That(order.Status, Is.EqualTo(OrderStatus.Draft));
    }
}
