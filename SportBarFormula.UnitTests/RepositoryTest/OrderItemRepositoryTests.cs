using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

[TestFixture]
public class OrderItemRepositoryTests
{
    private SportBarFormulaDbContext _dbContext;
    private OrderItemRepository _orderItemRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();
        _orderItemRepository = new OrderItemRepository(_dbContext);
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
            OrderItemId = 4,
            OrderId = 1, // Existing order
            MenuItemId = 1, // Existing menu item
            Quantity = 2,
            Price = 11.98m
        };

        // Act
        await _orderItemRepository.AddAsync(newOrderItem);

        // Assert
        var order = await _dbContext.Orders.Include(o => o.OrderItems).FirstAsync(o => o.OrderId == 1);
        Assert.That(order.OrderItems.Count, Is.EqualTo(3));
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
        Assert.That(order.OrderItems.Count, Is.EqualTo(1));
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
        Assert.That(orderItem.Price, Is.EqualTo(8.99m));
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
        Assert.That(orderItems.Count(), Is.EqualTo(3));
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
            OrderItemId = 4,
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

    /// <summary>
    /// Tests if DeleteAsync throws KeyNotFoundException when the OrderItem does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenOrderItemDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _orderItemRepository.DeleteAsync(999));
        Assert.That(exception.Message, Is.EqualTo("OrderItem not found"));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws KeyNotFoundException when the OrderItem does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenOrderItemDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _orderItemRepository.GetByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("OrderItem not found"));
    }

    /// <summary>
    /// Tests if UpdateAsync throws ArgumentNullException when the OrderItem entity is null.
    /// </summary>
    [Test]
    public void UpdateAsync_ShouldThrowArgumentNullException_WhenOrderItemIsNull()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _orderItemRepository.UpdateAsync(null));
        Assert.That(exception.ParamName, Is.EqualTo("entity"));
        Assert.That(exception.Message, Does.Contain("OrderItem entity is null"));
    }
}
