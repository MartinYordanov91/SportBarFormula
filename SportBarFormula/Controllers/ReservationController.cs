using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;

namespace SportBarFormula.Controllers;

/// <summary>
/// Booking Management Controller.
/// </summary>
public class ReservationController(
    ILogger<ReservationController> logger,
    IReservationService service
    ) : Controller
{

    private readonly IReservationService _service = service;
    private readonly ILogger<ReservationController> _logger = logger;


    /// <summary>
    /// List of all reservations for admins.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var reservations = await _service.GetAllReservationsAsync();
        return View(reservations);
    }
}
