using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.ViewModels.MenuItem;

public class MenuItemListViewModel
{
    public IEnumerable<MenuItemCardViewModel> MenuItems { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    public int? SelectedCategoryId { get; set; }
}
