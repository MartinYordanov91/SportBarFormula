﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infrastructure.Constants.DataConstants.MenuItemConstants;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("Contains information about menu items - food, drinks and more.")]
public class MenuItem
{
    public MenuItem()
    {
        this.OrderItems = new HashSet<OrderItem>();
    }

    [Key]
    [Comment("Unique identifier of the item")]
    public int MenuItemId { get; set; }

    [MaxLength(MenuItemNameMaxLength)]
    [Comment("Item name")]
    public required string Name { get; set; }

    [MaxLength(MenuItemDescriptionMaxLength)]
    [Comment("Item description")]
    public string? Description { get; set; }

    [Column(TypeName = MenuItemPricenPrecision)]
    [Comment("Item price")]
    public required decimal Price { get; set; }

    [Comment("Shows how many grams or how many milliliters the given product is")]
    public int Quantity { get; set; }

    [ForeignKey(nameof(Category))]
    [Comment("Unique identifier of the Category")]
    public int CategoryId { get; set; }

    [MaxLength(MenuItemImageURLMaxLength)]
    [Comment("Item Image URL")]
    public required string ImageURL { get; set; }

    [Comment("Item availability flag")]
    public bool IsAvailable { get; set; } = true;

    [MaxLength(MenuItemIngredientsMaxLength)]
    [Comment("List of ingredients")]
    public string? Ingredients { get; set; }

    [Comment("Preparation time in minutes")]
    public required int PreparationTime { get; set; }

    [Comment("Soft delit flag")]
    public bool IsDeleted { get; set; } = false;



    [Comment("Item category (drink, pizza, etc.)")]
    public Category Category { get; set; } = null!;

    [Comment("Collection of order items associated with this menu item")]
    public virtual ICollection<OrderItem> OrderItems { get; set; }
}
