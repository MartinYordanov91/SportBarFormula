using SportBarFormula.Core.ViewModels.MenuItem;

namespace SportBarFormula.Core.ServiceModel.MenuIrem;

public class AllMenuItemFilteredAndPagetVielModels
{
    public AllMenuItemFilteredAndPagetVielModels()
    {
        this.MenuItems = new HashSet<MenuItemCardViewModel>();
    }

    public int TotalMenuItemsCount { get; set; }

    public IEnumerable<MenuItemCardViewModel> MenuItems { get; set; }
}
