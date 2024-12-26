using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportBarFormula.Infrastructure.Data.Models;


[Comment("Manages table reservations in the sports bar.")]
public class Reservation
{
    [Key]
    [Comment("Unique identifier of the reservation")]
    public int ReservationId { get; set; }

    [Comment("Identifier of the user who made the reservation")]
    public required string UserId { get; set; }

    [Comment("Date and time of the reservation")]
    public required DateTime ReservationDate { get; set; }

    [Comment("Identifier of the reserved table")]
    public int? TableId { get; set; }

    [Comment("shows where the table is (indoor, outdoor)")]
    public bool IsIndor { get; set; }

    [Comment("Number of guests")]
    public required int NumberOfGuests { get; set; }

    [Comment("Indicates whether the reservation is canceled")]
    public bool IsCanceled { get; set; } = false;



    [ForeignKey(nameof(TableId))]
    [Comment("Details of the reserved table")]
    public virtual Table? Table { get; set; }

    [ForeignKey(nameof(UserId))]
    [Comment("Details of the user who made the reservation")]
    public virtual IdentityUser User { get; set; } = null!;
}

