using System.ComponentModel.DataAnnotations;

namespace SportBarFormula.Core.ViewModels.Reservation;

public class ReservationViewModel
{
    public int ReservationId { get; set; }

    public required string UserId { get; set; }

    [Required]
    [Display(Name = "Дата на резервация")]
    public string ReservationDate { get; set; } = string.Empty;

    public int? TableId { get; set; }

    [Display(Name = "Маса")]
    public bool IsIndor { get; set; } =true;

    [Display(Name = "Брой гости")]
    public int NumberOfGuests { get; set; }

    public bool IsCanceled { get; set; } = false;
}
