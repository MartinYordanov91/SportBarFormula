using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

[TestFixture]
public class MenuItemRepositoryTests
{
    private SportBarFormulaDbContext _dbContext;
    private MenuItemRepository _menuItemRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();
        _menuItemRepository = new MenuItemRepository(_dbContext);
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
    /// Tests if GetAllAsync returns all menu items.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllMenuItems()
    {
        // Act
        var result = await _menuItemRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(4));
        CollectionAssert.AreEquivalent(new[] { "Margherita Pizza", "Coke", "Cheesecake", "Burger" }, result.Select(mi => mi.Name));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a menu item when it exists.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnMenuItem_WhenMenuItemExists()
    {
        // Act
        var result = await _menuItemRepository.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Margherita Pizza"));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws a KeyNotFoundException when the menu item does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenMenuItemDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _menuItemRepository.GetByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("MenuItem not found"));
    }

    /// <summary>
    /// Tests if AddAsync adds a new menu item.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { MenuItemId = 5, Name = "New MenuItem", Price = 12.5m, CategoryId = 1, ImageURL = "image5.jpg", Quantity = 300, PreparationTime = 20 };

        // Act
        await _menuItemRepository.AddAsync(menuItem);
        var result = await _dbContext.MenuItems.FindAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("New MenuItem"));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing menu item.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateMenuItem()
    {
        // Arrange
        var menuItem = await _menuItemRepository.GetByIdAsync(1);
        menuItem.Name = "Updated Name";

        // Act
        await _menuItemRepository.UpdateAsync(menuItem);
        var result = await _dbContext.MenuItems.FindAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
    }

    /// <summary>
    /// Tests if DeleteAsync sets IsDeleted to true when the menu item exists.
    /// </summary> 
    [Test]
    public async Task DeleteAsync_ShouldSetIsDeletedToTrue_WhenMenuItemExists()
    {  
        //Act 
        await _menuItemRepository.DeleteAsync(1);
        var result = await _dbContext.MenuItems.FindAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.IsDeleted, Is.True);
    }

    /// <summary>
    /// Tests if DeleteAsync throws a KeyNotFoundException when the menu item does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenMenuItemDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _menuItemRepository.DeleteAsync(999));
        Assert.That(exception.Message, Is.EqualTo("MenuItem not found"));
    }
}
