using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infastructure.Constants.DataConstants.OrdersConstants;

namespace SportBarFormula.Infastructure.Data.Models;

[Comment("Contains information about orders placed by customers.")]
public class Orders
{
    [Key]
    [Comment("Unique identifier of the order")]
    public int OrderId { get; set; }

    [Comment("Identifier of the user who placed the order")]
    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required virtual IdentityUser User { get; set; }

    [Comment("Order date")]
    public required DateTime OrderDate { get; set; }

    [Column(TypeName = OrderTotalAmountPrecision)]
    [Comment("Total amount of the order")]
    public required decimal TotalAmount { get; set; }
}
