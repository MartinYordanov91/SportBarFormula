using System.ComponentModel.DataAnnotations;
using static SportBarFormula.Infrastructure.Constants.DataConstants.MenuItemConstants;

namespace SportBarFormula.Core.ViewModels.MenuItem;

public class CreateMenuItemViewModel
{
    [StringLength(MenuItemNameMaxLength, MinimumLength = MenuItemNameMinLength)]
    public required string Name { get; set; }

    [StringLength(MenuItemDescriptionMaxLength, MinimumLength = MenuItemDescriptionMinLength)]
    public string? Description { get; set; }

    [Range(typeof(decimal), MenuItemPriceMin, MenuItemPricenMax, ConvertValueInInvariantCulture = true)]
    public required decimal Price { get; set; }

    [Range(typeof(int), MenuItemQuantityMin, MenuItemQuantityMax)]
    public int Quantity { get; set; }

    public int CategoryId { get; set; }

    [StringLength(MenuItemImageURLMaxLength , MinimumLength = MenuItemImageURLMinLength)]
    public required string ImageURL { get; set; }

    [StringLength(MenuItemIngredientsMaxLength , MinimumLength = MenuItemIngredientsMinLength)]
    public string? Ingredients { get; set; }

    [Range(typeof(int), MenuItemPreparationTimeMin, MenuItemPreparationTimeMax)]
    public required int PreparationTime { get; set; }

}
