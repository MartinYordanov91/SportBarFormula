﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infrastructure.Constants.DataConstants.OrderItemConstants;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("This table is the link between Orders and MenuItems. Each line in it represents one item in the order")]
public class OrderItem
{
    [Key]
    [Comment("Unique identifier of the OrderItem")]
    public int OrderItemId { get; set; }

    [Comment("Order ID (foreign key to Orders table)")]
    public required int OrderId { get; set; }

    [Comment("Item ID (foreign key to MenuItems table)")]
    public required int MenuItemId { get; set; }

    [Comment("Item quantity")]
    public required int Quantity { get; set; }

    [Column(TypeName = OrderItemPricenPrecision)]
    [Comment("Item price at time of order vs. quantity")]
    public required decimal Price { get; set; }



    [ForeignKey(nameof(MenuItemId))]
    [Comment("Details of the menu item associated with this order item")]
    public  virtual MenuItem MenuItem { get; set; } = null!;

    [ForeignKey(nameof(OrderId))]
    [Comment("Details of the order associated with this order item")]
    public virtual Order Order { get; set; } = null!;
}
