using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Core.ViewModels.MenuItem;

public class AllMenuItemsQueryModel
{
    public AllMenuItemsQueryModel()
    {
        this.CurrentPage = 1;
        this.MenuItemsPerPage = 4;

        this.Categories = new HashSet<string>();
        this.Menuitems = new HashSet<MenuItemCardViewModel>();
    }

    [Display(Name = "Категоруя")]
    public string? Category { get; set; }

    [Display(Name = "Търсене по текст")]
    public string? SerchString { get; set; }

    [Display(Name = "Сортирай по")]
    public MenuItemSorting MenuItemSorting { get; set; }

    public int CurrentPage { get; set; }

    public int MenuItemsPerPage { get; set; }

    public int TotalMenuItems { get; set; }

    public IEnumerable<string> Categories { get; set; }

    public IEnumerable<MenuItemCardViewModel> Menuitems { get; set; }

}
