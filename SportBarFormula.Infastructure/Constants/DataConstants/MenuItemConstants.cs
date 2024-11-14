using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Infrastructure.Constants.DataConstants;

public static class MenuItemConstants
{
    public const int MenuItemNameMinLength = 3;
    public const int MenuItemNameMaxLength = 150;
    //[StringLength(MenuItemNameMaxLength, MinimumLength = MenuItemNameMinLength)]

    public const int MenuItemDescriptionMinLength = 0;
    public const int MenuItemDescriptionMaxLength = 500;
    //[StringLength(MenuItemDescriptionMaxLength, MinimumLength = MenuItemDescriptionMinLength)]

    public const string MenuItemPriceMin = "0.00";
    public const string MenuItemPricenMax = "200.00";
    public const string MenuItemPricenPrecision = "decimal(18,2)";
    //[Range(typeof(decimal), MenuItemPriceMin, MenuItemPricenMax, ConvertValueInInvariantCulture = true)]

    public const int MenuItemImageURLMinLength = 3;
    public const int MenuItemImageURLMaxLength = 500;
    //[StringLength(MenuItemImageURLMaxLength, MinimumLength = MenuItemImageURLMinLength)]

    public const string MenuItemQuantityMin = "0";
    public const string MenuItemQuantityMax = "2000";
    //[Range(typeof(int), MenuItemQuantityMin, MenuItemQuantityMax)]

    public const int MenuItemIngredientsMinLength = 0;
    public const int MenuItemIngredientsMaxLength = 500;
    //[StringLength(MenuItemIngredientsMaxLength, MinimumLength = MenuItemIngredientsMinLength)]

    public const string MenuItemPreparationTimeMin = "5";
    public const string MenuItemPreparationTimeMax = "30";
    //[Range(typeof(int), MenuItemPreparationTimeMin, MenuItemPreparationTimeMax)]
}
