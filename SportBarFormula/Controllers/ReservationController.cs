using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Reservation;
using System.Security.Claims;

namespace SportBarFormula.Controllers;

/// <summary>
/// Booking Management Controller.
/// </summary>
public class ReservationController(
   IModelStateLoggerService logger,
    IReservationService service
    ) : Controller
{

    private readonly IReservationService _service = service;
    private readonly IModelStateLoggerService _logger = logger;

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

    //--------------------------------------------------------------------------------------------------------> Create
    /// <summary>
    ///Form to create a new reservation.
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        var userId = User.Id();

        var model = new ReservationViewModel()
        {
            UserId = userId
        };

        return View(model);
    }

    /// <summary>
    /// Saves the new reservation.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(ReservationViewModel model)
    {

        var userId = User.Id();
        model.UserId = userId;

        if (!ModelState.IsValid)
        {
            _logger.LogModelErrors(ModelState);

            return View(model);
        }

        await _service.AddReservationAsync(model);

        return RedirectToAction(nameof(MyReservations));
    }
}
