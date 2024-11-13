﻿using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Infrastructure.Constants.DataConstants;

public static class MenuItemConstants
{
    public const int MenuItemNameMinLength = 3;
    public const int MenuItemNameMaxLength = 150;

    public const int MenuItemDescriptionMinLength = 0;
    public const int MenuItemDescriptionMaxLength = 500;

    public const string MenuItemPriceMin = "0.00";
    public const string MenuItemPricenMax = "200.00";
    public const string MenuItemPricenPrecision = "decimal(18,2)";
    //[Range(typeof(decimal), MenuItemPriceMin, MenuItemPricenMax, ConvertValueInInvariantCulture = true)]

    public const int MenuItemImageURLMinLength = 3;
    public const int MenuItemImageURLMaxLength = 500;

    public const int MenuItemQuantityMin = 0;
    public const int MenuItemQuantityMax = 2000;

    public const int MenuItemIngredientsMinLength = 0;
    public const int MenuItemIngredientsMaxLength = 500;

    public const string MenuItemPreparationTimeMin = "5";
    public const string MenuItemPreparationTimeMax = "30";
    //[Range(typeof(int), MenuItemPreparationTimeMin, MenuItemPreparationTimeMax)]
}
