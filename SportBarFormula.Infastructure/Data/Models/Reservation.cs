using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Infastructure.Data.Models;


[Comment("Manages table reservations in the sports bar.")]
public class Reservation
{
    [Key]
    [Comment("Unique identifier of the reservation")]
    public int ReservationId { get; set; }

    [Comment("Identifier of the user who made the reservation")]
    public required string UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required virtual IdentityUser User { get; set; }

    [Comment("Date and time of the reservation")]
    public required DateTime ReservationDate { get; set; }

    [Comment("Identifier of the reserved table")]
    public required int TableId { get; set; }

    [ForeignKey(nameof(TableId))]
    public required virtual Table Table { get; set; }

    [Comment("Number of guests")]
    public required int NumberOfGuests { get; set; }

    [Comment("Indicates whether the reservation is canceled")]
    public bool IsCanceled { get; set; } = false;
}

