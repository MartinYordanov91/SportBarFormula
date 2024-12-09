using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.RepositoryTest;

[TestFixture]
public class CategoryRepositoryTests
{
    private SportBarFormulaDbContext _dbContext;
    private CategoryRepository _categoryRepository;

    /// <summary>
    /// Sets up an in-memory database and repository before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();
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
        // Act
        var result = await _categoryRepository.GetAllAsync();

        // Assert
        Assert.That(result.Count(), Is.EqualTo(4));
        CollectionAssert.AreEquivalent(new[] { "Pizza", "Drinks", "Desserts", "Category to Delete" }, result.Select(c => c.Name));
    }

    /// <summary>
    /// Tests if GetByIdAsync returns a category when it exists.
    /// </summary>
    [Test]
    public async Task GetByIdAsync_ShouldReturnCategory_WhenCategoryExists()
    {
        // Act
        var result = await _categoryRepository.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Pizza"));
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
        var category = new Category { CategoryId = 5, Name = "New Category" };

        // Act
        await _categoryRepository.AddAsync(category);
        var result = await _dbContext.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("New Category"));
    }

    /// <summary>
    /// Tests if UpdateAsync updates an existing category.
    /// </summary>
    [Test]
    public async Task UpdateAsync_ShouldUpdateCategory()
    {
        // Arrange
        var category = await _categoryRepository.GetByIdAsync(1);
        category.Name = "Updated Name";

        // Act
        await _categoryRepository.UpdateAsync(category);
        var result = await _dbContext.Categories.FindAsync(category.CategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Name"));
    }

    /// <summary>
    /// Tests if DeleteAsync removes a category when it exists and has no related menu items.
    /// </summary>
    [Test]
    public async Task DeleteAsync_ShouldRemoveCategory_WhenCategoryExistsAndHasNoMenuItems()
    {
        // Act
        await _categoryRepository.DeleteAsync(4);
        var result = await _dbContext.Categories.FindAsync(4);

        // Assert
        Assert.That(result, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteAsync throws InvalidOperationException when the category has related menu items.
    /// </summary>
    [Test]
    public void DeleteAsync_ShouldThrowInvalidOperationException_WhenCategoryHasMenuItems()
    {
        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryRepository.DeleteAsync(1));
        Assert.That(exception.Message, Does.Contain("The association between entity types 'Category' and 'MenuItem'"));
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
