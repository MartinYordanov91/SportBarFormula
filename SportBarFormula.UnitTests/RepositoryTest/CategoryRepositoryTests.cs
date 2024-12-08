using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;

[TestFixture]
public class CategoryRepositoryTests
{
    private DbContextOptions<SportBarFormulaDbContext> _options;
    private SportBarFormulaDbContext _dbContext;
    private CategoryRepository _categoryRepository;

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
        _categoryRepository = new CategoryRepository(_dbContext);
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
    /// Tests if GetAllAsync returns all categories.
    /// </summary>
    [Test]
    public async Task GetAllAsync_ShouldReturnAllCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { CategoryId = 1, Name = "Category 1" },
            new Category { CategoryId = 2, Name = "Category 2" }
        };

        await _dbContext.Categories.AddRangeAsync(categories);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _categoryRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(categories.Count));
        CollectionAssert.AreEquivalent(categories.Select(c => c.Name), result.Select(c => c.Name));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a category when it exists.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { CategoryId = 1, Name = "Test Category" };
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _categoryRepository.GetByIdAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(category.Name));
    }

    /// <summary>
    /// Tests if GetByIdAsync throws a KeyNotFoundException when the category does not exist.
    /// </summary>
    [Test]
    public void GetByIdAsync_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _categoryRepository.GetByIdAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Category not found"));
    }

    /// <summary>
    /// Tests if AddAsync adds a new category.
    /// </summary>
    [Test]
    public async Task AddAsync_ShouldAddCategory()
    {
        // Arrange
        var category = new Category { CategoryId = 1, Name = "New Category" };

        // Act
        await _categoryRepository.AddAsync(category);
        var result = await _dbContext.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(category.Name));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing category.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        var category = new Category { CategoryId = 1, Name = "Old Name" };
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        category.Name = "Updated Name";

        // Act
        await _categoryRepository.UpdateAsync(category);
        var result = await _dbContext.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
    }

    /// <summary>
    /// Tests if DeleteAsync removes a category when it exists.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveCategory_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { CategoryId = 1, Name = "Category to Delete" };
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();

        // Act
        await _categoryRepository.DeleteAsync(category.CategoryId);
        var result = await _dbContext.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteAsync throws a KeyNotFoundException when the category does not exist.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExist()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _categoryRepository.DeleteAsync(999));
        Assert.That(exception.Message, Is.EqualTo("Category not found"));
    }
}
