namespace SportBarFormula.Core.ViewModels.MenuItem;
public class MenuItemDetailsViewModel
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Category { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string ImageURL { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string Ingredients { get; set; } = string.Empty;
    public int PreparationTime { get; set; }
}
