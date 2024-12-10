using SportBarFormula.Core.ServiceModel.MenuIrem;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.MenuItem;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services;

public class MenuItemService(IRepository<MenuItem> repository) : IMenuItemService
{
    private readonly IRepository<MenuItem> _repository = repository;

    /// <summary>
    /// Adds a new menu item to the repository.
    /// </summary>
    /// <param name="model">The view model containing menu item details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddMenuItemAsync(CreateMenuItemViewModel model)
    {
        MenuItem newMenuItem = new MenuItem()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity,
            ImageURL = model.ImageURL,
            CategoryId = model.CategoryId,
            Ingredients = model.Ingredients,
            PreparationTime = model.PreparationTime,
            IsDeleted = false,
            IsAvailable = true,
        };

        await _repository.AddAsync(newMenuItem);
    }

    /// <summary>
    /// Retrieves the details of a menu item by its ID.
    /// </summary>
    /// <param name="id">The ID of the menu item.</param>
    /// <returns>The view model containing menu item details.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the menu item is not found.</exception>
    public async Task<MenuItemDetailsViewModel> GetMenuItemDetailsByIdAsync(int id)
    {
        MenuItem menuItem;

        try
        {
            menuItem = await _repository.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No MenuItem found in the repository.");
        }

        var viewModel = new MenuItemDetailsViewModel
        {
            MenuItemId = menuItem.MenuItemId,
            Name = menuItem.Name,
            Description = menuItem.Description ?? "",
            Price = menuItem.Price,
            Quantity = menuItem.Quantity,
            Category = menuItem.Category.Name,
            CategoryId = menuItem.CategoryId,
            ImageURL = menuItem.ImageURL,
            IsAvailable = menuItem.IsAvailable,
            Ingredients = menuItem.Ingredients ?? "",
            PreparationTime = menuItem.PreparationTime
        };

        return viewModel;
    }

    /// <summary>
    /// Retrieves deleted menu items by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A collection of deleted menu item view models.</returns>
    public async Task<ICollection<MenuItemCardViewModel>> GetDeletedItemsByCategoryAsync(int? categoryId)
    {
        var menuItems = await _repository.GetAllAsync();

        var filteredMenuItems = menuItems
            .Where(mi => !categoryId.HasValue || mi.CategoryId == categoryId)
            .Where(mi => mi.IsDeleted == true)
            .Select(mi => new MenuItemCardViewModel
            {
                MenuItemId = mi.MenuItemId,
                Name = mi.Name,
                ImageURL = mi.ImageURL,
                Ingredients = mi.Ingredients,
                Price = mi.Price,
                PreparationTime = mi.PreparationTime,
                Quantity = mi.Quantity,
                CategoryId = mi.CategoryId
            })
            .ToList();

        return filteredMenuItems;
    }

    /// <summary>
    /// Retrieves menu items by category ID.
    /// </summary>
    /// <param name="categoryId">The category ID.</param>
    /// <returns>A collection of menu item view models.</returns>
    public async Task<ICollection<MenuItemCardViewModel>> GetMenuItemsByCategoryAsync(int? categoryId)
    {
        var menuItems = await _repository.GetAllAsync();

        var filteredMenuItems = menuItems
            .Where(mi => !categoryId.HasValue || mi.CategoryId == categoryId)
            .Where(mi => mi.IsDeleted == false)
            .Select(mi => new MenuItemCardViewModel
            {
                MenuItemId = mi.MenuItemId,
                Name = mi.Name,
                ImageURL = mi.ImageURL,
                Ingredients = mi.Ingredients,
                Price = mi.Price,
                PreparationTime = mi.PreparationTime,
                Quantity = mi.Quantity,
                CategoryId = mi.CategoryId
            })
            .ToList();

        return filteredMenuItems;
    }

    /// <summary>
    /// Retrieves the edit form view model for a menu item by its ID.
    /// </summary>
    /// <param name="id">The ID of the menu item.</param>
    /// <returns>The edit form view model.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the menu item is not found.</exception>
    public async Task<MenuItemEditViewModel> GetMenuItemEditFormByIdAsync(int id)
    {
        MenuItem currentItem;

        try
        {
            currentItem = await _repository.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No MenuItem found in the repository.");
        }

        var model = new MenuItemEditViewModel
        {
            MenuItemId = currentItem.MenuItemId,
            Name = currentItem.Name,
            Description = currentItem.Description,
            Category = currentItem.Category.Name,
            CategoryId = currentItem.CategoryId,
            Price = currentItem.Price,
            Quantity = currentItem.Quantity,
            PreparationTime = currentItem.PreparationTime,
            ImageURL = currentItem.ImageURL,
            Ingredients = currentItem.Ingredients,
            IsAvailable = currentItem.IsAvailable,
            IsDeleted = currentItem.IsDeleted,
        };

        return model;
    }

    /// <summary>
    /// Updates an existing menu item.
    /// </summary>
    /// <param name="model">The view model containing updated menu item details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the menu item is not found.</exception>
    public async Task UpdateMenuItemAsync(MenuItemEditViewModel model)
    {
        MenuItem existingMenuItem;

        try
        {
            existingMenuItem = await _repository.GetByIdAsync(model.MenuItemId);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("No MenuItem found in the repository.");
        }

        existingMenuItem.Name = model.Name;
        existingMenuItem.Description = model.Description;
        existingMenuItem.Price = model.Price;
        existingMenuItem.Quantity = model.Quantity;
        existingMenuItem.ImageURL = model.ImageURL;
        existingMenuItem.CategoryId = model.CategoryId;
        existingMenuItem.Ingredients = model.Ingredients;
        existingMenuItem.PreparationTime = model.PreparationTime;
        existingMenuItem.IsDeleted = model.IsDeleted;
        existingMenuItem.IsAvailable = model.IsAvailable;

        await _repository.UpdateAsync(existingMenuItem);
    }


    /// <summary>
    /// Undeletes a menu item by its ID.
    /// </summary>
    /// <param name="id">The ID of the menu item.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the menu item is not found.</exception>
    public async Task UnDeleteItemAsync(int id)
    {
        MenuItem unDeleteItem;

        try
        {
            unDeleteItem = await _repository.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException( "No MenuItem found in the repository.");
        }

        unDeleteItem.IsDeleted = false;

        await _repository.UpdateAsync(unDeleteItem);
    }


    /// <summary>
    /// Retrieves all menu items with filtering, sorting, and pagination.
    /// </summary>
    /// <param name="queryModel">The query model containing filter, search, and sorting options.</param>
    /// <returns>The view model containing filtered and paginated menu items.</returns>
    public async Task<AllMenuItemFilteredAndPagetVielModels> AllAsync(AllMenuItemsQueryModel queryModel)
    {
        var menuitems = await _repository.GetAllAsync();

        menuitems = menuitems
            .Where(mi => mi.IsDeleted == false);

        if (!string.IsNullOrWhiteSpace(queryModel.Category))
        {
            menuitems = menuitems
                .Where(mi => mi.Category.Name == queryModel.Category)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(queryModel.SerchString))
        {
            menuitems = menuitems
                .Where(mi =>
                mi.Name.ToLower().Contains(queryModel.SerchString.ToLower()) ||
                (mi.Description ?? string.Empty).ToLower().Contains(queryModel.SerchString.ToLower()) ||
                (mi.Ingredients ?? string.Empty).ToLower().Contains(queryModel.SerchString.ToLower())
                ).ToList();
        }

        menuitems = queryModel.MenuItemSorting switch
        {
            MenuItemSorting.PriceAsending =>
            menuitems.OrderBy(mi => mi.Price),
            MenuItemSorting.PriceDsending =>
            menuitems.OrderByDescending(mi => mi.Price),
            MenuItemSorting.TimePreparationAsending =>
            menuitems.OrderBy(mi => mi.PreparationTime),
            MenuItemSorting.TimePreparationDsending =>
            menuitems.OrderByDescending(mi => mi.PreparationTime),
            _ => menuitems.OrderBy(mi => mi.Name)
        };

        var menuItemsToCurrentPage = menuitems
            .Skip((queryModel.CurrentPage -1) * queryModel.MenuItemsPerPage)
            .Take(queryModel.MenuItemsPerPage)
            .Select(mi => new MenuItemCardViewModel
            {
                MenuItemId = mi.MenuItemId,
                Name = mi.Name,
                ImageURL = mi.ImageURL,
                Ingredients = mi.Ingredients,
                Price = mi.Price,
                PreparationTime = mi.PreparationTime,
                Quantity = mi.Quantity,
                CategoryId = mi.CategoryId
            })
            .ToList();

        return new AllMenuItemFilteredAndPagetVielModels()
        {
            TotalMenuItemsCount = menuitems.Count(),
            MenuItems = menuItemsToCurrentPage,
        };
    }
}
