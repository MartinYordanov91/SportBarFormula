namespace SportBarFormula.Core.ViewModels.MenuItem;

public class MenuItemCardViewModel
{
    public int MenuItemId { get; set; }

    public required string Name { get; set; }

    public required string ImageURL { get; set; }

    public string? Ingredients { get; set; }

    public required decimal Price { get; set; }

    public required int PreparationTime { get; set; }

    public int Quantity { get; set; }

    public int CategoryId { get; set; }
}
