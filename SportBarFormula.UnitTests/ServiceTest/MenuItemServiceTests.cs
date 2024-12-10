using SportBarFormula.Core.Services;
using SportBarFormula.Core.ViewModels.MenuItem;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.ServiceTest;

[TestFixture]
public class MenuItemServiceTests
{
    private SportBarFormulaDbContext _dbContext;
    private MenuItemService _menuItemService;

    [SetUp]
    public void SetUp()
    {
        // Създаваме InMemory база данни
        _dbContext = MockDbContextFactory.Create();

        // Създаваме MenuItemService с реален DbContext
        var repository = new MenuItemRepository(_dbContext);
        _menuItemService = new MenuItemService(repository);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }


    /// <summary>
    /// Tests if AddMenuItemAsync adds a new menu item to the database.
    /// </summary>
    [Test]
    public async Task AddMenuItemAsync_ShouldAddMenuItemToDatabase()
    {
        // Arrange
        var newMenuItem = new CreateMenuItemViewModel
        {
            Name = "Test MenuItem",
            Description = "Test Description",
            Price = 9.99m,
            Quantity = 10,
            ImageURL = "test.jpg",
            CategoryId = 1,
            Ingredients = "Test Ingredients",
            PreparationTime = 20
        };

        // Act
        await _menuItemService.AddMenuItemAsync(newMenuItem);

        // Assert
        var addedMenuItem = _dbContext.MenuItems.FirstOrDefault(mi => mi.Name == "Test MenuItem");
        Assert.That(addedMenuItem, Is.Not.Null);
        Assert.That(addedMenuItem.Name, Is.EqualTo(newMenuItem.Name));
        Assert.That(addedMenuItem.Price, Is.EqualTo(newMenuItem.Price));
    }

    /// <summary>
    /// Tests if GetMenuItemDetailsByIdAsync returns the correct details of the menu item.
    /// </summary>
    [Test]
    public async Task GetMenuItemDetailsByIdAsync_ShouldReturnCorrectDetails()
    {
        // Arrange
        var existingMenuItemId = 1;

        // Act
        var result = await _menuItemService.GetMenuItemDetailsByIdAsync(existingMenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Margherita Pizza"));
    }

    /// <summary>
    /// Tests if GetMenuItemDetailsByIdAsync throws InvalidOperationException when the menu item is not found.
    /// </summary>
    [Test]
    public void GetMenuItemDetailsByIdAsync_ShouldThrowArgumentNullException_WhenItemNotFound()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _menuItemService.GetMenuItemDetailsByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("No MenuItem found in the repository."));
    }

    /// <summary>
    /// Tests if GetDeletedItemsByCategoryAsync returns only the deleted items.
    /// </summary>
    [Test]
    public async Task GetDeletedItemsByCategoryAsync_ShouldReturnOnlyDeletedItems()
    {
        // Arrange
        var menuItem = _dbContext.MenuItems.First();
        menuItem.IsDeleted = true;
        _dbContext.SaveChanges();

        // Act
        var result = await _menuItemService.GetDeletedItemsByCategoryAsync(null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(menuItem.Name));
    }

    /// <summary>
    /// Tests if GetMenuItemsByCategoryAsync returns only the non-deleted items.
    /// </summary>
    [Test]
    public async Task GetMenuItemsByCategoryAsync_ShouldReturnOnlyNonDeletedItems()
    {
        // Arrange
        var menuItem = _dbContext.MenuItems.First();
        menuItem.IsDeleted = true;
        _dbContext.SaveChanges();

        // Act
        var result = await _menuItemService.GetMenuItemsByCategoryAsync(null);

        // Assert
        Assert.That(result.Count, Is.EqualTo(_dbContext.MenuItems.Count(mi => !mi.IsDeleted)));
    }

    /// <summary>
    /// Tests if GetMenuItemEditFormByIdAsync returns the correct edit form for the menu item.
    /// </summary>
    [Test]
    public async Task GetMenuItemEditFormByIdAsync_ShouldReturnCorrectEditForm()
    {
        // Arrange
        var existingMenuItemId = 1;

        // Act
        var result = await _menuItemService.GetMenuItemEditFormByIdAsync(existingMenuItemId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Margherita Pizza"));
    }

    /// <summary>
    /// Tests if GetMenuItemEditFormByIdAsync throws InvalidOperationException when the menu item is not found.
    /// </summary>
    [Test]
    public void GetMenuItemEditFormByIdAsync_ShouldThrowException_WhenItemNotFound()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _menuItemService.GetMenuItemEditFormByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("No MenuItem found in the repository."));
    }

    /// <summary>
    /// Tests if UpdateMenuItemAsync updates the menu item correctly.
    /// </summary>
    [Test]
    public async Task UpdateMenuItemAsync_ShouldUpdateMenuItemCorrectly()
    {
        // Arrange
        var menuItemId = 1;
        var updateModel = new MenuItemEditViewModel
        {
            MenuItemId = menuItemId,
            Name = "Updated Name",
            Description = "Updated Description",
            Price = 12.99m,
            Quantity = 5,
            ImageURL = "updated.jpg",
            CategoryId = 2,
            Category = "Drinks",
            Ingredients = "Updated Ingredients",
            PreparationTime = 25,
            IsDeleted = false,
            IsAvailable = true
        };

        // Act
        await _menuItemService.UpdateMenuItemAsync(updateModel);

        // Assert
        var updatedMenuItem = _dbContext.MenuItems.First(mi => mi.MenuItemId == menuItemId);
        Assert.That(updatedMenuItem.Name, Is.EqualTo(updateModel.Name));
        Assert.That(updatedMenuItem.Price, Is.EqualTo(updateModel.Price));
    }

    /// <summary>
    /// Tests if UpdateMenuItemAsync throws InvalidOperationException when the menu item is not found.
    /// </summary>
    [Test]
    public void UpdateMenuItemAsync_ShouldThrowException_WhenItemNotFound()
    {
        // Arrange
        var updateModel = new MenuItemEditViewModel
        {
            MenuItemId = 999, // Несъществуващ ID
            Name = "Updated Item",
            Description = "Updated Description",
            Price = 12.99m,
            Quantity = 10,
            ImageURL = "http://example.com/newimage.jpg",
            CategoryId = 2,
            Category = "Drinks",
            Ingredients = "Updated Ingredients",
            PreparationTime = 20,
            IsAvailable = false,
            IsDeleted = true
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _menuItemService.UpdateMenuItemAsync(updateModel));
        Assert.That(exception.Message, Is.EqualTo("No MenuItem found in the repository."));
    }

    /// <summary>
    /// Tests if UnDeleteItemAsync undeletes the menu item.
    /// </summary>
    [Test]
    public async Task UnDeleteItemAsync_ShouldUndeleteMenuItem()
    {
        // Arrange
        var menuItemId = 1;
        var menuItem = _dbContext.MenuItems.First(mi => mi.MenuItemId == menuItemId);
        menuItem.IsDeleted = true;
        _dbContext.SaveChanges();

        // Act
        await _menuItemService.UnDeleteItemAsync(menuItemId);

        // Assert
        var undeletedMenuItem = _dbContext.MenuItems.First(mi => mi.MenuItemId == menuItemId);
        Assert.That(undeletedMenuItem.IsDeleted, Is.False);
    }

    /// <summary>
    /// Tests if UnDeleteItemAsync throws InvalidOperationException when the menu item is not found.
    /// </summary>
    [Test]
    public void UnDeleteItemAsync_ShouldThrowException_WhenItemNotFound()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _menuItemService.UnDeleteItemAsync(999));
        Assert.That(exception.Message, Is.EqualTo("No MenuItem found in the repository."));
    }

    /// <summary>
    /// Tests if AllAsync returns filtered and paged results.
    /// </summary>
    [Test]
    public async Task AllAsync_ShouldReturnFilteredAndPagedResults()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            Category = "Pizza",
            SerchString = "Margherita",
            MenuItemSorting = MenuItemSorting.PriceAsending,
            CurrentPage = 1,
            MenuItemsPerPage = 10
        };

        // Act
        var result = await _menuItemService.AllAsync(queryModel);

        // Assert
        Assert.That(result.TotalMenuItemsCount, Is.GreaterThan(0));
        Assert.That(result.MenuItems.Count, Is.LessThanOrEqualTo(queryModel.MenuItemsPerPage));
    }

    /// <summary>
    /// Tests if AllAsync returns filtered and paged results sorted by price descending.
    /// </summary>
    [Test]
    public async Task AllAsync_ShouldReturnFilteredAndPagedResults_SortedByPriceDescending()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            Category = "Pizza",
            SerchString = "Margherita",
            MenuItemSorting = MenuItemSorting.PriceDsending,
            CurrentPage = 1,
            MenuItemsPerPage = 10
        };

        // Act
        var result = await _menuItemService.AllAsync(queryModel);

        // Assert
        Assert.That(result.TotalMenuItemsCount, Is.GreaterThan(0));
        Assert.That(result.MenuItems.Count, Is.LessThanOrEqualTo(queryModel.MenuItemsPerPage));
        Assert.That(result.MenuItems, Is.Ordered.Descending.By("Price"));
    }

    /// <summary>
    /// Tests if AllAsync returns filtered and paged results sorted by preparation time ascending.
    /// </summary>
    [Test]
    public async Task AllAsync_ShouldReturnFilteredAndPagedResults_SortedByPreparationTimeAscending()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            Category = "Pizza",
            SerchString = "Margherita",
            MenuItemSorting = MenuItemSorting.TimePreparationAsending,
            CurrentPage = 1,
            MenuItemsPerPage = 10
        };

        // Act
        var result = await _menuItemService.AllAsync(queryModel);

        // Assert
        Assert.That(result.TotalMenuItemsCount, Is.GreaterThan(0));
        Assert.That(result.MenuItems.Count, Is.LessThanOrEqualTo(queryModel.MenuItemsPerPage));
        Assert.That(result.MenuItems, Is.Ordered.Ascending.By("PreparationTime"));
    }

    /// <summary>
    /// Tests if AllAsync returns filtered and paged results sorted by preparation time descending.
    /// </summary>
    [Test]
    public async Task AllAsync_ShouldReturnFilteredAndPagedResults_SortedByPreparationTimeDescending()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            Category = "Pizza",
            SerchString = "Margherita",
            MenuItemSorting = MenuItemSorting.TimePreparationDsending,
            CurrentPage = 1,
            MenuItemsPerPage = 10
        };

        // Act
        var result = await _menuItemService.AllAsync(queryModel);

        // Assert
        Assert.That(result.TotalMenuItemsCount, Is.GreaterThan(0));
        Assert.That(result.MenuItems.Count, Is.LessThanOrEqualTo(queryModel.MenuItemsPerPage));
        Assert.That(result.MenuItems, Is.Ordered.Descending.By("PreparationTime"));
    }

    /// <summary>
    /// Tests if AllAsync returns filtered and paged results sorted by name (default).
    /// </summary>
    [Test]
    public async Task AllAsync_ShouldReturnFilteredAndPagedResults_SortedByName()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            Category = "Pizza",
            SerchString = "Margherita",
            MenuItemSorting = MenuItemSorting.Default,
            CurrentPage = 1,
            MenuItemsPerPage = 10
        };

        // Act
        var result = await _menuItemService.AllAsync(queryModel);

        // Assert
        Assert.That(result.TotalMenuItemsCount, Is.GreaterThan(0));
        Assert.That(result.MenuItems.Count, Is.LessThanOrEqualTo(queryModel.MenuItemsPerPage));
        Assert.That(result.MenuItems, Is.Ordered.By("Name"));
    }

}