using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Core.Services.Contracts;

public interface IMenuItemService
{
    Task<MenuItemDetailsViewModel> GetMenuItemDetailsByIdAsync(int id);

    Task<ICollection<MenuItemCardViewModel>> GetMenuItemsByCategoryAsync(int? categoryId);

    Task<ICollection<MenuItemCardViewModel>> GetDeletedItemsByCategoryAsync(int? categoryId);

    Task<MenuItemEditViewModel> GetMenuItemEditFormByIdAsync(int id);

    Task AddMenuItemAsync(CreateMenuItemViewModel model);

    Task UpdateMenuItemAsync(MenuItemEditViewModel model);

    Task UnDeleteItemAsync(int id);
}
