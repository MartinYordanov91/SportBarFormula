using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Core.Services.Contracts;

public interface IMenuItemService
{
    Task<MenuItemDetailsViewModel> GetMenuItemDetailsByIdAsync(int id);

    Task<ICollection<MenuItemCardViewModel>> GetCardMenuItemsAsync();

    Task<ICollection<MenuItemCardViewModel>> GetMenuItemsByCategoryAsync(int? categoryId);

    Task<MenuItemViewModel> GetMenuItemByIdAsync(int id);

    Task AddMenuItemAsync(CreateMenuItemViewModel model);
}
