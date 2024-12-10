using Moq;
using SportBarFormula.Core.Services;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.ServiceTest;

public class CategoryServiceTests
{
    private SportBarFormulaDbContext _dbContext;
    private CategoryService _categoryService;

    [SetUp]
    public void SetUp()
    {
        _dbContext = MockDbContextFactory.Create();

        var repository = new CategoryRepository(_dbContext);
        _categoryService = new CategoryService(repository);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests if GetAllCategoriesAsync returns all categories.
    /// </summary>
    [Test]
    public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
    {
        // Act
        var result = await _categoryService.GetAllCategoriesAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(_dbContext.Categories.Count()));

        foreach (var category in result)
        {
            var expectedCategory = _dbContext.Categories.Find(category.CategoryId);
            Assert.That(expectedCategory, Is.Not.Null);
            Assert.That(category.Name, Is.EqualTo(expectedCategory.Name));
        }
    }

    /// <summary>
    /// Tests if AddCategoryAsync adds a new category to the database.
    /// </summary>
    [Test]
    public async Task AddCategoryAsync_ShouldAddCategoryToDatabase()
    {
        // Arrange
        var newCategory = new CategoryViewModel
        {
            Name = "New Category"
        };

        // Act
        await _categoryService.AddCategoryAsync(newCategory);

        // Assert
        var addedCategory = _dbContext.Categories.FirstOrDefault(c => c.Name == "New Category");
        Assert.That(addedCategory, Is.Not.Null);
        Assert.That(addedCategory.Name, Is.EqualTo(newCategory.Name));
    }

    /// <summary>
    /// Tests if GetCategoryByIdAsync returns the correct category by ID.
    /// </summary>
    [Test]
    public async Task GetCategoryByIdAsync_ShouldReturnCorrectCategory()
    {
        // Arrange
        var existingCategoryId = 1;

        // Act
        var result = await _categoryService.GetCategoryByIdAsync(existingCategoryId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.CategoryId, Is.EqualTo(existingCategoryId));
        Assert.That(result.Name, Is.EqualTo("Pizza")); // assuming "Pizza" is the name of the category with ID 1
    }

    /// <summary>
    /// Tests if GetCategoryByIdAsync throws InvalidOperationException when category is not found.
    /// </summary>
    [Test]
    public void GetCategoryByIdAsync_ShouldThrowInvalidOperationException_WhenCategoryNotFound()
    {
        // Arrange
        var nonExistingCategoryId = 999; // ID, който не съществува

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.GetCategoryByIdAsync(nonExistingCategoryId));
        Assert.That(exception.Message, Is.EqualTo("No categories found in the repository."));
    }

    /// <summary>
    /// Tests if UpdateCategoryAsync updates the category correctly.
    /// </summary>
    [Test]
    public async Task UpdateCategoryAsync_ShouldUpdateCategory()
    {
        // Arrange
        var existingCategoryId = 1;
        var updatedCategoryModel = new CategoryViewModel
        {
            CategoryId = existingCategoryId,
            Name = "Updated Category"
        };

        // Act
        await _categoryService.UpdateCategoryAsync(updatedCategoryModel);

        // Assert
        var updatedCategory = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == existingCategoryId);
        Assert.That(updatedCategory, Is.Not.Null);
        Assert.That(updatedCategory.Name, Is.EqualTo(updatedCategoryModel.Name));
    }

    /// <summary>
    /// Tests if UpdateCategoryAsync throws InvalidOperationException when category is not found.
    /// </summary>
    [Test]
    public void UpdateCategoryAsync_ShouldThrowInvalidOperationException_WhenCategoryNotFound()
    {
        // Arrange
        var nonExistingCategoryId = 999; // ID, който не съществува
        var updatedCategoryModel = new CategoryViewModel
        {
            CategoryId = nonExistingCategoryId,
            Name = "Non-Existent Category"
        };

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.UpdateCategoryAsync(updatedCategoryModel));
        Assert.That(exception.Message, Is.EqualTo("No categories found in the repository."));
    }

    /// <summary>
    /// Tests if DeleteCategoryAsync deletes the category when it has no associated menu items.
    /// </summary>
    [Test]
    public async Task DeleteCategoryAsync_ShouldDeleteCategory_WhenNoAssociatedMenuItems()
    {
        // Arrange
        var categoryToDeleteId = 4; // ID на категорията, която няма асоциирани меню елементи

        // Act
        await _categoryService.DeleteCategoryAsync(categoryToDeleteId);

        // Assert
        var deletedCategory = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryToDeleteId);
        Assert.That(deletedCategory, Is.Null);
    }

    /// <summary>
    /// Tests if DeleteCategoryAsync throws InvalidOperationException when category is not found.
    /// </summary>
    [Test]
    public void DeleteCategoryAsync_ShouldThrowInvalidOperationException_WhenCategoryNotFound()
    {
        // Arrange
        var nonExistingCategoryId = 999; // ID, който не съществува

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.DeleteCategoryAsync(nonExistingCategoryId));
        Assert.That(exception.Message, Is.EqualTo("No categories found in the repository."));
    }

    /// <summary>
    /// Tests if DeleteCategoryAsync throws InvalidOperationException when category has associated menu items.
    /// </summary>
    [Test]
    public void DeleteCategoryAsync_ShouldThrowInvalidOperationException_WhenCategoryHasAssociatedMenuItems()
    {
        // Arrange
        var categoryIdWithMenuItems = 1; // ID на категория с асоциирани меню елементи

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _categoryService.DeleteCategoryAsync(categoryIdWithMenuItems));
        Assert.That(exception.Message, Is.EqualTo("Cannot delete category because it has associated menu items."));
    }
    /// <summary>
    /// Tests if GetCategoryByNameAsync returns the correct category by name.
    /// </summary>
    [Test]
    public async Task GetCategoryByNameAsync_ShouldReturnCorrectCategory()
    {
        // Arrange
        var existingCategoryName = "Pizza";

        // Act
        var result = await _categoryService.GetCategoryByNameAsync(existingCategoryName);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(existingCategoryName));
    }

    /// <summary>
    /// Tests if GetCategoryByNameAsync throws InvalidOperationException when no categories are found in the repository.
    /// </summary>
    [Test]
    public void GetCategoryByNameAsync_ShouldThrowInvalidOperationException_WhenNoCategoriesFound()
    {
        // За този тест трябва да се създаде контекст без категории.
        var emptyDbContext = MockDbContextFactory.CreateEmpty();
        var repository = new CategoryRepository(emptyDbContext);
        var categoryService = new CategoryService(repository);

        // Act & Assert
        var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await categoryService.GetCategoryByNameAsync("NonExistentCategory"));
        Assert.That(exception.Message, Is.EqualTo("No categories found in the repository."));
    }


    /// <summary>
    /// Tests if GetCategoryByNameAsync throws ArgumentException when category is not found.
    /// </summary>
    [Test]
    public void GetCategoryByNameAsync_ShouldThrowArgumentException_WhenCategoryNotFound()
    {
        // Arrange
        var nonExistingCategoryName = "NonExistentCategory";

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryService.GetCategoryByNameAsync(nonExistingCategoryName));
        Assert.That(exception.Message, Is.EqualTo($"Category with name '{nonExistingCategoryName}' not found."));
    }

}
