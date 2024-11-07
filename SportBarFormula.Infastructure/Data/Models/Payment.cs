using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SportBarFormula.Infrastructure.Constants.DataConstants.PaymentConstants;

namespace SportBarFormula.Infrastructure.Data.Models;

[Comment("Tracks information about payments for orders.")]
public class Payment
{
    [Key]
    [Comment("Unique identifier of the payment")]
    public int PaymentId { get; set; }

    [Comment("Identifier of the order associated with the payment")]
    public required int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public required virtual Order Order { get; set; }

    [Comment("Identifier of the user who made the payment")]
    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required virtual IdentityUser User { get; set; }

    [Comment("Date and time of the payment")]
    public required DateTime PaymentDate { get; set; }

    [Column(TypeName = PaymantAmountPrecision)]
    [Comment("Amount of the payment")]
    public required decimal Amount { get; set; }

    [MaxLength(PaymentMethodMaxLength)]
    [Comment("Payment method (e.g., cash, card)")]
    public required string PaymentMethod { get; set; }

    [MaxLength(PaymentStatusMaxLength)]
    [Comment("Payment status (e.g., successful, unsuccessful)")]
    public required string Status { get; set; }
}

