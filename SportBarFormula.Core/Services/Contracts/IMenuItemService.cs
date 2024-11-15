using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Core.Services.Contracts;

public interface IMenuItemService
{
    Task<ICollection<MenuItemViewModel>> GetAllMenuItemsAsync();

    Task<ICollection<MenuItemCardViewModel>> GetCardMenuItemsAsync();

    Task<MenuItemViewModel> GetMenuItemByIdAsync(int id);

    Task AddMenuItem(CreateMenuItemViewModel model);
}
