using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infrastructure.Constants.DataConstants.OrderConstants;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("Contains information about orders placed by customers.")]
public class Order
{
    public Order()
    {
        this.OrderItems = new HashSet<OrderItem>();
        this.Payments = new HashSet<Payment>();
    }

    [Key]
    [Comment("Unique identifier of the order")]
    public int OrderId { get; set; }

    [Comment("Identifier of the user who placed the order")]
    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual IdentityUser User { get; set; } = null!;

    [Comment("Order date")]
    public required DateTime OrderDate { get; set; }

    [Column(TypeName = OrderTotalAmountPrecision)]
    [Comment("Total amount of the order")]
    public required decimal TotalAmount { get; set; }

    [Comment("Status of the order")]
    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    [Comment("Collection of order items")]
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    [Comment("Collection of payments associated with this order")] 
    public virtual ICollection<Payment> Payments { get; set; }
}
