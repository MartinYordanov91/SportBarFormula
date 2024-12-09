using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Enums;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

[TestFixture]
public class OrderRepositoryTests
{
    private SportBarFormulaDbContext _dbContext;
    private OrderRepository _orderRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();
        _orderRepository = new OrderRepository(_dbContext);
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
    /// Tests if AddAsync adds a new Order.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddOrder()
    {
        // Arrange
        var newOrder = new Order
        {
            OrderId = 4,
            UserId = "user2",
            OrderDate = DateTime.UtcNow,
            TotalAmount = 20.00m,
            Status = OrderStatus.Draft
        };

        // Act
        await _orderRepository.AddAsync(newOrder);
        var result = await _dbContext.Orders.FindAsync(newOrder.OrderId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.UserId, Is.EqualTo("user2"));
        Assert.That(result.TotalAmount, Is.EqualTo(20.00m));
    }

    /// <summary>
    /// Tests if DeleteAsync removes an Order.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveOrder()
    {
        // Act
        await _orderRepository.DeleteAsync(3);
        var result = await _dbContext.Orders.FindAsync(3);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteAsync throws KeyNotFoundException when the Order does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenOrderDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _orderRepository.DeleteAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Order not found"));
    }

    /// <summary>
    /// Tests if GetAllAsync returns all Orders.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllOrders()
    {
        // Act
        var result = await _orderRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns an Order with its associated OrderItems.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnOrder_WithOrderItems()
    {
        // Act
        var result = await _orderRepository.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.OrderItems.Count, Is.EqualTo(2));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws KeyNotFoundException when the Order does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenOrderDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _orderRepository.GetByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Order not found"));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing Order.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateOrder()
    {
        // Arrange
        var order = await _orderRepository.GetByIdAsync(1);
        order.TotalAmount = 25.00m;

        // Act
        await _orderRepository.UpdateAsync(order);
        var result = await _dbContext.Orders.FindAsync(order.OrderId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.TotalAmount, Is.EqualTo(25.00m));
    }

    /// <summary>
    /// Tests if UpdateAsync throws ArgumentNullException when the Order entity is null.
    /// </summary>
    [Test]
    public void UpdateAsync_ShouldThrowArgumentNullException_WhenOrderIsNull()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _orderRepository.UpdateAsync(null));
        Assert.That(exception.ParamName, Is.EqualTo("entity"));
        Assert.That(exception.Message, Does.Contain("Order is null"));
    }
}
