using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;

[TestFixture]
public class MenuItemRepositoryTests
{
    private DbContextOptions<SportBarFormulaDbContext> _options;
    private SportBarFormulaDbContext _dbContext;
    private MenuItemRepository _menuItemRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        // Set up an in-memory database
        _options = new DbContextOptionsBuilder<SportBarFormulaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new SportBarFormulaDbContext(_options);
        _menuItemRepository = new MenuItemRepository(_dbContext);

        // Seed data
        _dbContext.Categories.Add(new Category { CategoryId = 1, Name = "Pizza" });
        _dbContext.Categories.Add(new Category { CategoryId = 2, Name = "Drinks" });
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
    /// Tests if GetAllAsync returns all menu items.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllMenuItems()
    {
        // Arrange
        var menuItems = new List<MenuItem>
        {
            new MenuItem { MenuItemId = 1, Name = "Margarita", Price = 10.5m, CategoryId = 1, ImageURL = "image1.jpg", Quantity = 500, PreparationTime = 15 },
            new MenuItem { MenuItemId = 2, Name = "Coke", Price = 2.0m, CategoryId = 2, ImageURL = "image2.jpg", Quantity = 330, PreparationTime = 0 }
        };

        await _dbContext.MenuItems.AddRangeAsync(menuItems);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _menuItemRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(menuItems.Count));
        CollectionAssert.AreEquivalent(menuItems.Select(mi => mi.Name), result.Select(mi => mi.Name));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a menu item when it exists.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnMenuItem_WhenMenuItemExists()
    {
        // Arrange
        var menuItem = new MenuItem { MenuItemId = 1, Name = "Margarita", Price = 10.5m, CategoryId = 1, ImageURL = "image1.jpg", Quantity = 500, PreparationTime = 15 };
        await _dbContext.MenuItems.AddAsync(menuItem);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _menuItemRepository.GetByIdAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(menuItem.Name));
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
        var menuItem = new MenuItem { MenuItemId = 1, Name = "Margarita", Price = 10.5m, CategoryId = 1, ImageURL = "image1.jpg", Quantity = 500, PreparationTime = 15 };

        // Act
        await _menuItemRepository.AddAsync(menuItem);
        var result = await _dbContext.MenuItems.FindAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(menuItem.Name));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing menu item.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateMenuItem()
    {
        // Arrange
        var menuItem = new MenuItem { MenuItemId = 1, Name = "Old Name", Price = 10.5m, CategoryId = 1, ImageURL = "image1.jpg", Quantity = 500, PreparationTime = 15 };
        await _dbContext.MenuItems.AddAsync(menuItem);
        await _dbContext.SaveChangesAsync();

        menuItem.Name = "Updated Name";

        // Act
        await _menuItemRepository.UpdateAsync(menuItem);
        var result = await _dbContext.MenuItems.FindAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
    }

    /// <summary>
    /// Tests if DeleteAsync removes a menu item when it exists.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveMenuItem_WhenMenuItemExists()
    {
        // Arrange
        var menuItem = new MenuItem { MenuItemId = 1, Name = "Margarita", Price = 10.5m, CategoryId = 1, ImageURL = "image1.jpg", Quantity = 500, PreparationTime = 15 };
        await _dbContext.MenuItems.AddAsync(menuItem);
        await _dbContext.SaveChangesAsync();

        // Act
        await _menuItemRepository.DeleteAsync(menuItem.MenuItemId);
        var result = await _dbContext.MenuItems.FindAsync(menuItem.MenuItemId);

        // Assert
        Assert.That(result, Is.Null);
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
