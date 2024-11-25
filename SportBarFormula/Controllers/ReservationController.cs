using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Reservation;
using System.Security.Claims;

namespace SportBarFormula.Controllers;

/// <summary>
/// Booking Management Controller.
/// </summary>
/// <param name="logger"></param>
/// <param name="service"></param>
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

    //--------------------------------------------------------------------------------------------------------> Edit
    /// <summary>
    /// Displays the edit view for a specific reservation.
    /// </summary>
    /// <param name="id">The ID of the reservation to edit.</param>
    /// <returns>An IActionResult representing the edit view or NotFound if the reservation is not found.</returns>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _service.GetReservationByIdAsync(id);

        if (reservation == null)
        {
            return NotFound();
        }

        ViewBag.IsIndorOptions = new SelectList(
            new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Вътре", Selected = reservation.IsIndor },
                new SelectListItem { Value = "false", Text = "Вън", Selected = !reservation.IsIndor }
            }, "Value", "Text", reservation.IsIndor
        );

        ViewBag.IsCanceledOptions = new SelectList(
            new List<SelectListItem> { 
                new SelectListItem { Value = "false", Text = "Активна", Selected = !reservation.IsCanceled },
                new SelectListItem { Value = "true", Text = "Анулирана", Selected = reservation.IsCanceled } 
            }, "Value", "Text", reservation.IsCanceled
        );

        return View(reservation);
    }

    /// <summary>
    /// Processes the edit form submission for a specific reservation.
    /// </summary>
    /// <param name="model">The updated reservation view model.</param>
    /// <returns>An IActionResult representing the result of the operation.</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(ReservationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogModelErrors(ModelState);
            return View(model);
        }

        await _service.UpdateReservationAsync(model);

        return RedirectToAction(nameof(Index));
    }

    //--------------------------------------------------------------------------------------------------------> Cancel
    /// <summary>
    /// Cancels a reservation by calling the CancelReservationAsync method and redirects to the "MyReservations" view.
    /// </summary>
    /// <param name="id">The ID of the reservation to cancel.</param>
    /// <returns>An IActionResult that represents a redirection to the "MyReservations" view after the cancellation is processed.</returns>
    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        await _service.CancelReservationAsync(id);

        return RedirectToAction(nameof(MyReservations));
    }

}


