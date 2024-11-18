using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Core.ViewModels.Reservation;

public class ReservationViewModel
{
    public int ReservationId { get; set; }

    public required string UserId { get; set; }

    [Display(Name = "Дата на резервация")]
    [RegularExpression(@"^\d{2}-\d{2}-\d{4} \d{2}:\d{2}$")]
    public required DateTime ReservationDate { get; set; }

    [Display(Name = "Маса")]
    public int TableId { get; set; }

    [Display(Name = "Брой гости")]
    public int NumberOfGuests { get; set; }

    public bool IsCanceled { get; set; } = false;
}
