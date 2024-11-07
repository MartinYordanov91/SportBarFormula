using Microsoft.EntityFrameworkCore;
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

    [ForeignKey(nameof(OrderId))]
    public required virtual Order Order { get; set; }

    [Comment("Item ID (foreign key to MenuItems table)")]
    public required int MenuItemId { get; set; }

    [ForeignKey(nameof(MenuItemId))]
    public required virtual MenuItem MenuItem { get; set; }

    [Comment("Item quantity")]
    public required int Quantity { get; set; }

    [Column(TypeName = OrderItemPricenPrecision)]
    [Comment("Item price at time of order vs. quantity")]
    public required decimal Price { get; set; }

}
