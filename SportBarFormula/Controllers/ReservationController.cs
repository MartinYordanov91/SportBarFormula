using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Reservation;
using System.Security.Claims;

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

    //--------------------------------------------------------------------------------------------------------> Index
    /// <summary>
    /// List of all reservations for admins.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var reservations = await _service.GetAllReservationsAsync();
        return View(reservations);
    }

    //--------------------------------------------------------------------------------------------------------> MyReservations
    /// <summary>
    /// Shows customers their current and past bookings.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> MyReservations()
    {
        string userId = this.User.Id();
        var reservations = await _service.GetReservationsByUserIdAsync(userId);

        return View(reservations);
    }

}
