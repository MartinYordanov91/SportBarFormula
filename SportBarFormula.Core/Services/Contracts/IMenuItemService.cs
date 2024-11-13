using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Core.Services.Contracts;

public interface IMenuItemService
{
    Task<ICollection<MenuItemViewModel>> GetAllMenuItemsAsync();

    Task<MenuItemViewModel> GetMenuItemByIdAsync(int id);
}
