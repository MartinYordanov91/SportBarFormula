using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportBarFormula.Controllers;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.UnitTests.Moq;

namespace SportBarFormula.UnitTests.Controllers;

/// <summary>
/// Unit tests for the CategoryController.
/// </summary>
[TestFixture]
public class CategoryControllerTests
{
    private Mock<ICategoryService> _mockCategoryService;
    private Mock<IModelStateLoggerService> _mockLoggerService;
    private CategoryController _categoryController;
    private SportBarFormulaDbContext _dbContext;

    /// <summary>
    /// Sets up the test environment before each test.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _mockLoggerService = new Mock<IModelStateLoggerService>();
        _categoryController = new CategoryController(_mockCategoryService.Object, _mockLoggerService.Object);
        _dbContext = MockDbContextFactory.Create();
    }

    /// <summary>
    /// Cleans up the test environment after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _categoryController.Dispose();
        _dbContext.Dispose();
    }

    /// <summary>
    /// Tests if the Index method returns a ViewResult with all categories.
    /// </summary>
    [Test]
    public async Task Index_ShouldReturnViewResult_WithAllCategories()
    {
        // Arrange
        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Category 1" },
            new CategoryViewModel { CategoryId = 2, Name = "Category 2" }
        };

        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync())
            .ReturnsAsync(categories);

        // Act
        var result = await _categoryController.Index();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(categories));
    }

    /// <summary>
    /// Tests if the Index method returns an empty list when no categories are available.
    /// </summary>
    [Test]
    public async Task Index_ShouldReturnViewResult_WithEmptyList_WhenNoCategoriesAvailable()
    {
        // Arrange
        var categories = new List<CategoryViewModel>();
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync())
            .ReturnsAsync(categories);

        // Act
        var result = await _categoryController.Index();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(categories));
    }

    /// <summary>
    /// Tests if the Create (GET) method returns a ViewResult.
    /// </summary>
    [Test]
    public void Create_Get_ShouldReturnViewResult()
    {
        // Act
        var result = _categoryController.Create();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
    }

    /// <summary>
    /// Tests if the Create (POST) method redirects to Index on success.
    /// </summary>
    [Test]
    public async Task Create_Post_ShouldRedirectToIndex_OnSuccess()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };

        // Act
        var result = await _categoryController.Create(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult.ActionName, Is.EqualTo(nameof(_categoryController.Index)));
    }

    /// <summary>
    /// Tests if the Create (POST) method returns the same view with model on failure.
    /// </summary>
    [Test]
    public async Task Create_Post_ShouldReturnViewResult_WithModel_OnFailure()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _categoryController.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _categoryController.Create(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(categoryViewModel));
    }

    /// <summary>
    /// Tests if the Create (POST) method returns BadRequestResult on InvalidOperationException.
    /// </summary>
    [Test]
    public async Task Create_Post_ShouldReturnBadRequestResult_OnInvalidOperationException()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _mockCategoryService.Setup(service => service.AddCategoryAsync(categoryViewModel))
            .ThrowsAsync(new InvalidOperationException());

        // Act
        var result = await _categoryController.Create(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    /// <summary>
    /// Tests if the Create (POST) method returns UnauthorizedResult on UnauthorizedAccessException.
    /// </summary>
    [Test]
    public async Task Create_Post_ShouldReturnUnauthorizedResult_OnUnauthorizedAccessException()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _mockCategoryService.Setup(service => service.AddCategoryAsync(categoryViewModel))
            .ThrowsAsync(new UnauthorizedAccessException());

        // Act
        var result = await _categoryController.Create(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<UnauthorizedResult>());
    }

    /// <summary>
    /// Tests if the Edit (GET) method returns a ViewResult with the category model.
    /// </summary>
    [Test]
    public async Task Edit_Get_ShouldReturnViewResult_WithCategoryModel()
    {
        // Arrange
        var categoryId = 1;
        var categoryViewModel = new CategoryViewModel { CategoryId = categoryId, Name = "Category 1" };
        _mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(categoryViewModel);

        // Act
        var result = await _categoryController.Edit(categoryId);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(categoryViewModel));
    }

    /// <summary>
    /// Tests if the Edit (GET) method returns NotFoundResult when the category is not found.
    /// </summary>
    [Test]
    public async Task Edit_Get_ShouldReturnNotFoundResult_WhenCategoryNotFound()
    {
        // Arrange
        var categoryId = 999;
        _mockCategoryService.Setup(service => service.GetCategoryByIdAsync(categoryId))
            .ReturnsAsync(null as CategoryViewModel);


        // Act
        var result = await _categoryController.Edit(categoryId);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }



    /// <summary>
    /// Tests if the Edit (POST) method redirects to Index on success.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldRedirectToIndex_OnSuccess()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };

        // Act
        var result = await _categoryController.Edit(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult.ActionName, Is.EqualTo(nameof(_categoryController.Index)));
    }

    /// <summary>
    /// Tests if the Edit (POST) method returns the same view with model on failure.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldReturnViewResult_WithModel_OnFailure()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _categoryController.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _categoryController.Edit(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult.Model, Is.EqualTo(categoryViewModel));
    }

    /// <summary>
    /// Tests if the Edit (POST) method returns BadRequestResult on InvalidOperationException.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldReturnBadRequestResult_OnInvalidOperationException()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _mockCategoryService.Setup(service => service.UpdateCategoryAsync(categoryViewModel))
            .ThrowsAsync(new InvalidOperationException());

        // Act
        var result = await _categoryController.Edit(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    /// <summary>
    /// Tests if the Edit (POST) method returns UnauthorizedResult on UnauthorizedAccessException.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldReturnUnauthorizedResult_OnUnauthorizedAccessException()
    {
        // Arrange
        var categoryViewModel = new CategoryViewModel { CategoryId = 1, Name = "Category 1" };
        _mockCategoryService.Setup(service => service.UpdateCategoryAsync(categoryViewModel))
            .ThrowsAsync(new UnauthorizedAccessException());

        // Act
        var result = await _categoryController.Edit(categoryViewModel);

        // Assert
        Assert.That(result, Is.TypeOf<UnauthorizedResult>());
    }

    /// <summary>
    /// Tests if the Delete (POST) method redirects to Index on success.
    /// </summary>
    [Test]
    public async Task Delete_Post_ShouldRedirectToIndex_OnSuccess()
    {
        // Arrange
        var categoryId = 1;
        _mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
        .Returns(Task.CompletedTask);
        
        // Act
        var result = await _categoryController.Delete(categoryId);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult.ActionName, Is.EqualTo(nameof(_categoryController.Index)));
    }

    /// <summary>
    /// Tests if the Delete (POST) method sets TempData error message on exception and redirects to Index.
    /// </summary>
    [Test]
    public async Task Delete_ShouldRedirectToIndexWithErrorMessage_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = 999; // Несъществуващ ID
        var errorMessage = "Category not found";

        var tempData = new Mock<ITempDataDictionary>();
        _categoryController.TempData = tempData.Object;

        _mockCategoryService.Setup(service => service.DeleteCategoryAsync(categoryId))
            .ThrowsAsync(new InvalidOperationException(errorMessage));

        // Act
        var result = await _categoryController.Delete(categoryId);

        // Assert
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult!.ActionName, Is.EqualTo("Index"));

        tempData.VerifySet(td => td["ErrorMessage"] = errorMessage, Times.Once);
    }


}