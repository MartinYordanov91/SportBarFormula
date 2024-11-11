namespace SportBarFormula.Core.ViewModels;

public class MenuItemViewModel
{
    public required string Name { get; set; }

    public string? Description { get; set; }
    public required decimal Price { get; set; }

    public required string Category { get; set; }

    public required string ImageURL { get; set; }

    public bool IsAvailable { get; set; } = true;

    public string? Ingredients { get; set; }

    public required int PreparationTime { get; set; }

    public bool IsDeleted { get; set; } = false;
}
