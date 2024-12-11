using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using SportBarFormula.Controllers;
using SportBarFormula.Core.ServiceModel.MenuIrem;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Category;
using SportBarFormula.Core.ViewModels.MenuItem;
using X.PagedList;

namespace SportBarFormula.UnitTests.Controllers;



[TestFixture]
public class MenuControllerTests
{
    private Mock<IMenuItemService> _mockMenuItemService;
    private Mock<ICategoryService> _mockCategoryService;
    private Mock<IModelStateLoggerService> _mockLoggerService;
    private MenuController _menuController;

    [SetUp]
    public void SetUp()
    {
        _mockMenuItemService = new Mock<IMenuItemService>();
        _mockCategoryService = new Mock<ICategoryService>();
        _mockLoggerService = new Mock<IModelStateLoggerService>();
        _menuController = new MenuController(_mockMenuItemService.Object, _mockCategoryService.Object, _mockLoggerService.Object);
    }


    /// <summary>
    /// Cleans up the test environment after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _menuController.Dispose();
    }


    /// <summary>
    /// Tests if the Index action returns a ViewResult with filtered, sorted, and paginated menu items.
    /// </summary>
    [Test]
    public async Task Index_ShouldReturnViewResult_WithFilteredSortedPaginatedMenuItems()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            SerchString = "Pizza",
            MenuItemSorting = MenuItemSorting.Default,
            CurrentPage = 1,
            MenuItemsPerPage = 4
        };

        var serviceModel = new AllMenuItemFilteredAndPagetVielModels
        {
            MenuItems = new List<MenuItemCardViewModel>
            {
                new MenuItemCardViewModel {
                    MenuItemId = 1,
                    Name = "Pizza Margherita",
                    ImageURL = "margherita.jpg",
                    Ingredients = "Tomato, Mozzarella",
                    Price = 8.99m, PreparationTime = 15,
                    Quantity = 1, CategoryId = 1
                },

                new MenuItemCardViewModel {
                    MenuItemId = 2, Name = "Pizza Pepperoni",
                    ImageURL = "pepperoni.jpg",
                    Ingredients = "Tomato, Mozzarella, Pepperoni",
                    Price = 9.99m, PreparationTime = 15,
                    Quantity = 1,
                    CategoryId = 1
                }
            },
            TotalMenuItemsCount = 2
        };

        var categories = new List<CategoryViewModel>
    {
        new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
        new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
    };

        _mockMenuItemService.Setup(service => service.AllAsync(queryModel)).ReturnsAsync(serviceModel);
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Index(queryModel);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        var model = viewResult?.Model as AllMenuItemsQueryModel;
        Assert.That(model, Is.Not.Null);
        Assert.That(model!.Menuitems.Count(), Is.EqualTo(2));
        Assert.That(model.TotalMenuItems, Is.EqualTo(2));
        Assert.That(model.Categories.Count(), Is.EqualTo(2));
        Assert.That(model.Categories, Does.Contain("Pizza"));
    }
    /// <summary>
    /// Tests if the Details action returns a ViewResult with menu item details.
    /// </summary>
    [Test]
    public async Task Details_ShouldReturnViewResult_WithMenuItemDetails()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            MenuItemsPerPage = 4,
            Category = "Pizza",
            SerchString = "Pepperoni",
            MenuItemSorting = MenuItemSorting.Default,
            CurrentPage = 1
        };

        var menuItemDetails = new MenuItemDetailsViewModel
        {
            MenuItemId = 2,
            Name = "Pizza Pepperoni",
            ImageURL = "pepperoni.jpg",
            Ingredients = "Tomato, Mozzarella, Pepperoni",
            Price = 9.99m,
            PreparationTime = 15,
            Quantity = 1,
            CategoryId = 1
        };

        _mockMenuItemService.Setup(service => service.GetMenuItemDetailsByIdAsync(2)).ReturnsAsync(menuItemDetails);

        // Act
        var result = await _menuController.Details(queryModel, 2);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        var model = viewResult?.Model as MenuItemDetailsViewModel;
        Assert.That(model, Is.Not.Null);
        Assert.That(model!.MenuItemId, Is.EqualTo(2));
        Assert.That(model.Name, Is.EqualTo("Pizza Pepperoni"));
        Assert.That(model.ImageURL, Is.EqualTo("pepperoni.jpg"));
        Assert.That(model.Ingredients, Is.EqualTo("Tomato, Mozzarella, Pepperoni"));
        Assert.That(model.Price, Is.EqualTo(9.99m));
        Assert.That(model.PreparationTime, Is.EqualTo(15));
        Assert.That(model.Quantity, Is.EqualTo(1));
        Assert.That(model.CategoryId, Is.EqualTo(1));
    }


    /// <summary>
    /// Tests if the Details action returns NotFound when the menu item does not exist.
    /// </summary>
    [Test]
    public async Task Details_ShouldReturnNotFound_WhenMenuItemDoesNotExist()
    {
        // Arrange
        var queryModel = new AllMenuItemsQueryModel
        {
            MenuItemsPerPage = 4,
            Category = "Pizza",
            SerchString = "Pepperoni",
            MenuItemSorting = MenuItemSorting.Default,
            CurrentPage = 1
        };

        _mockMenuItemService.Setup(service => service.GetMenuItemDetailsByIdAsync(999))
            .ReturnsAsync((MenuItemDetailsViewModel?)null);

        // Act
        var result = await _menuController.Details(queryModel, 999);

        // Assert
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    /// <summary>
    /// Tests if the Create (GET) action returns a ViewResult with a SelectList of categories.
    /// </summary>
    [Test]
    public async Task Create_Get_ShouldReturnViewResult_WithSelectListOfCategories()
    {
        // Arrange
        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
            new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
        };
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Create();

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());

        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        Assert.That(viewResult!.ViewData["Categories"], Is.Not.Null);
        Assert.That(viewResult.ViewData["Categories"], Is.TypeOf<SelectList>());

        var selectList = viewResult.ViewData["Categories"] as SelectList;
        Assert.That(selectList, Is.Not.Null);
        Assert.That(selectList.Count(), Is.EqualTo(2));
        Assert.That(selectList.Any(i => i.Value == "1" && i.Text == "Pizza"), Is.True);
        Assert.That(selectList.Any(i => i.Value == "2" && i.Text == "Drinks"), Is.True);
    }



    /// <summary>
    /// Tests if the Create action returns a ViewResult when the model state is invalid.
    /// </summary>
    [Test]
    public async Task Create_ShouldReturnViewResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new CreateMenuItemViewModel
        {
            Name = "New Item",
            Price = 9.99m,
            Quantity = 1,
            CategoryId = 1,
            ImageURL = "newitem.jpg",
            PreparationTime = 15
        };
        _menuController.ModelState.AddModelError("Name", "Required");


        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
            new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
        };
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Create(model);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());

        var viewResult = result as ViewResult;
        var returnedModel = viewResult?.Model as CreateMenuItemViewModel;
        Assert.That(returnedModel, Is.EqualTo(model));
    }

    /// <summary>
    /// Tests if the Create (POST) action redirects to Index upon successful creation of a menu item.
    /// </summary>
    [Test]
    public async Task Create_Post_ShouldRedirectToIndex_OnSuccessfulCreation()
    {
        // Arrange
        var model = new CreateMenuItemViewModel
        {
            Name = "New Item",
            Price = 9.99m,
            Quantity = 1,
            CategoryId = 1,
            ImageURL = "newitem.jpg",
            PreparationTime = 15
        };

        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
            new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
        };
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Create(model);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult!.ActionName, Is.EqualTo(nameof(_menuController.Index)));
    }


    /// <summary>
    /// Tests if the Create action returns a ViewResult when an InvalidOperationException is thrown.
    /// </summary>
    [Test]
    public async Task Create_ShouldReturnViewResult_OnInvalidOperationException()
    {
        // Arrange
        var model = new CreateMenuItemViewModel
        {
            Name = "New Item",
            Price = 9.99m,
            Quantity = 1,
            CategoryId = 1,
            ImageURL = "newitem.jpg",
            PreparationTime = 15
        };

        _mockMenuItemService.Setup(service => service.AddMenuItemAsync(model)).ThrowsAsync(new InvalidOperationException("Error"));

        var categories = new List<CategoryViewModel>
    {
        new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
        new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
    };
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Create(model);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        var returnedModel = viewResult?.Model as CreateMenuItemViewModel;
        Assert.That(returnedModel, Is.EqualTo(model));
        Assert.That(_menuController.ModelState[string.Empty].Errors, Has.Some.Matches<ModelError>(e => e.ErrorMessage == "There was an error creating the menu item."));
    }

    /// <summary>
    /// Tests if the Create action returns Unauthorized when an UnauthorizedAccessException is thrown.
    /// </summary>
    [Test]
    public async Task Create_ShouldReturnUnauthorized_OnUnauthorizedAccessException()
    {
        // Arrange
        var model = new CreateMenuItemViewModel
        {
            Name = "New Item",
            Price = 9.99m,
            Quantity = 1,
            CategoryId = 1,
            ImageURL = "newitem.jpg",
            PreparationTime = 15
        };
        _mockMenuItemService.Setup(service => service.AddMenuItemAsync(model)).ThrowsAsync(new UnauthorizedAccessException());

        // Act
        var result = await _menuController.Create(model);

        // Assert
        Assert.That(result, Is.TypeOf<UnauthorizedResult>());
    }

    /// <summary>
    /// Tests if the Edit (GET) action returns a ViewResult with the menu item and SelectList of categories.
    /// </summary>
    [Test]
    public async Task Edit_Get_ShouldReturnViewResult_WithMenuItemAndSelectListOfCategories()
    {
        // Arrange
        var menuItemEditModel = new MenuItemEditViewModel
        {
            MenuItemId = 1,
            Name = "Pizza Margherita",
            Description = "Delicious classic pizza",
            Price = 8.99m,
            Quantity = 10,
            CategoryId = 1,
            Category = "Pizza",
            ImageURL = "margherita.jpg",
            Ingredients = "Tomato, Mozzarella",
            PreparationTime = 15,
            IsAvailable = true,
            IsDeleted = false
        };

        var categories = new List<CategoryViewModel>
    {
        new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
        new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
    };

        _mockMenuItemService.Setup(service => service.GetMenuItemEditFormByIdAsync(1)).ReturnsAsync(menuItemEditModel);
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Edit(1);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        var model = viewResult!.Model as MenuItemEditViewModel;
        Assert.That(model, Is.Not.Null);
        Assert.That(model!.MenuItemId, Is.EqualTo(1));
        Assert.That(model.Name, Is.EqualTo("Pizza Margherita"));

        var viewData = viewResult.ViewData;
        Assert.That(viewData["Categories"], Is.Not.Null);
        Assert.That(viewData["Categories"], Is.TypeOf<SelectList>());

        var selectList = viewData["Categories"] as SelectList;
        Assert.That(selectList, Is.Not.Null);
        Assert.That(selectList!.Count(), Is.EqualTo(2));
        Assert.That(selectList.Any(i => i.Value == "1" && i.Text == "Pizza"), Is.True);
        Assert.That(selectList.Any(i => i.Value == "2" && i.Text == "Drinks"), Is.True);
    }


    /// <summary>
    /// Tests if the Edit (POST) action returns a ViewResult with the same model when ModelState is invalid.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldReturnViewResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new MenuItemEditViewModel
        {
            MenuItemId = 1,
            Name = "Pizza Margherita",
            Description = "Delicious classic pizza",
            Price = 8.99m,
            Quantity = 10,
            CategoryId = 1,
            Category = "Pizza",
            ImageURL = "margherita.jpg",
            Ingredients = "Tomato, Mozzarella",
            PreparationTime = 15,
            IsAvailable = true,
            IsDeleted = false
        };
        _menuController.ModelState.AddModelError("Name", "Required");

        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
            new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
        };
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.Edit(model);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        var returnedModel = viewResult?.Model as MenuItemEditViewModel;
        Assert.That(returnedModel, Is.EqualTo(model));
        Assert.That(viewResult!.ViewData["Categories"], Is.Not.Null);
        Assert.That(viewResult.ViewData["Categories"], Is.TypeOf<SelectList>());
    }

    /// <summary>
    /// Tests if the Edit (POST) action redirects to Details upon successful update of a menu item.
    /// </summary>
    [Test]
    public async Task Edit_Post_ShouldRedirectToDetails_OnSuccessfulUpdate()
    {
        // Arrange
        var model = new MenuItemEditViewModel
        {
            MenuItemId = 1,
            Name = "Pizza Margherita",
            Description = "Delicious classic pizza",
            Price = 8.99m,
            Quantity = 10,
            CategoryId = 1,
            Category = "Pizza",
            ImageURL = "margherita.jpg",
            Ingredients = "Tomato, Mozzarella",
            PreparationTime = 15,
            IsAvailable = true,
            IsDeleted = false
        };

        // Act
        var result = await _menuController.Edit(model);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult!.ActionName, Is.EqualTo(nameof(_menuController.Details)));
        Assert.That(redirectResult.RouteValues!["id"], Is.EqualTo(model.MenuItemId));
    }

    /// <summary>
    /// Tests if the DeletedItems action returns a ViewResult with paginated list of deleted menu items and SelectList of categories.
    /// </summary>
    [Test]
    public async Task DeletedItems_ShouldReturnViewResult_WithPagedListAndSelectListOfCategories()
    {
        // Arrange
        var deletedItems = new List<MenuItemCardViewModel>
        {
            new MenuItemCardViewModel {
                MenuItemId = 1, Name = "Deleted Item 1",
                ImageURL = "deleted1.jpg",
                Ingredients = "Tomato",
                Price = 9.99m,
                PreparationTime = 15,
                Quantity = 0,
                CategoryId = 1
            },

            new MenuItemCardViewModel {
                MenuItemId = 2,
                Name = "Deleted Item 2",
                ImageURL = "deleted2.jpg",
                Ingredients = "Cheese",
                Price = 8.99m,
                PreparationTime = 10,
                Quantity = 0,
                CategoryId = 2
            }
        };

        var categories = new List<CategoryViewModel>
        {
            new CategoryViewModel { CategoryId = 1, Name = "Pizza" },
            new CategoryViewModel { CategoryId = 2, Name = "Drinks" }
        };

        _mockMenuItemService.Setup(service => service.GetDeletedItemsByCategoryAsync(null)).ReturnsAsync(deletedItems);
        _mockCategoryService.Setup(service => service.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _menuController.DeletedItems(null, 1);

        // Assert
        Assert.That(result, Is.TypeOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);

        var viewData = viewResult!.ViewData;
        Assert.That(viewData["Categories"], Is.Not.Null);
        Assert.That(viewData["Categories"], Is.TypeOf<SelectList>());
        Assert.That(viewData["SelectedCategoryId"], Is.Null);

        var pagedList = viewResult.Model as IPagedList<MenuItemCardViewModel>;
        Assert.That(pagedList, Is.Not.Null);
        Assert.That(pagedList!.Count, Is.EqualTo(2));
        Assert.That(pagedList.First().Name, Is.EqualTo("Deleted Item 1"));
        Assert.That(pagedList.ElementAt(1).Name, Is.EqualTo("Deleted Item 2"));
    }

    /// <summary>
    /// Tests if the UnDelete action restores a deleted item and redirects to Details.
    /// </summary>
    [Test]
    public async Task UnDelete_ShouldRestoreItemAndRedirectToDetails()
    {
        // Arrange
        int menuItemId = 1;

        // Act
        var result = await _menuController.UnDelete(menuItemId);

        // Assert
        _mockMenuItemService.Verify(service => service.UnDeleteItemAsync(menuItemId), Times.Once);
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        var redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult!.ActionName, Is.EqualTo(nameof(_menuController.Details)));
        Assert.That(redirectResult.RouteValues!["id"], Is.EqualTo(menuItemId));
    }

}



