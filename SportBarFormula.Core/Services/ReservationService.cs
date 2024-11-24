using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Reservation;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using System.Globalization;
using static SportBarFormula.Infrastructure.Constants.DataConstants.ReservationConstants;


namespace SportBarFormula.Core.Services;

/// <summary>
/// Reservation Management Service.
/// </summary>
public class ReservationService(IRepository<Reservation> repository) : IReservationService
{
    private readonly IRepository<Reservation> _repository = repository;


    /// <summary>
    /// Adds a new reservation asynchronously to the repository.
    /// </summary>
    /// <param name="model">The reservation view model containing the details of the reservation to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddReservationAsync(ReservationViewModel model)
    {
        if (!DateTime.TryParseExact(model.ReservationDate, ReservationDateTimeStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var reservationDate))
        {
            throw new ArgumentException("Invalid reservation date format.");
        }

        var reservation = new Reservation
        {
            UserId = model.UserId,
            ReservationDate = reservationDate,
            NumberOfGuests = model.NumberOfGuests,
            IsIndor = model.IsIndor,
        };

        await _repository.AddAsync(reservation);
    }

    /// <summary>
    /// Cancels the reservation by setting the IsCanceled flag to true.
    /// </summary>
    /// <param name="id">The ID of the reservation to cancel.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task CancelReservationAsync(int id)
    {
        var reservation = await _repository.GetByIdAsync(id);

        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }

        reservation.IsCanceled = true;

        await _repository.UpdateAsync(reservation);
    }

    /// <summary>
    /// Returns all reservations.
    /// </summary>
    /// <returns>A list of all reservations.</returns>
    public async Task<IEnumerable<ReservationViewModel>> GetAllReservationsAsync()
    {
        var reservations = await _repository.GetAllAsync();
        return reservations
            .Select(r => new ReservationViewModel
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ReservationDate = r.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
                TableId = r.TableId,
                NumberOfGuests = r.NumberOfGuests,
                IsCanceled = r.IsCanceled
            })
            .ToList();
    }

    /// <summary>
    /// Retrieves a reservation by its ID asynchronously and returns it as a ReservationViewModel.
    /// </summary>
    /// <param name="id">The ID of the reservation to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the ReservationViewModel with the reservation details.</returns>
    /// <exception cref="Exception">Thrown when the reservation is not found.</exception>
    public async Task<ReservationViewModel> GetReservationByIdAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);

        if (model == null)
        {
            throw new Exception("Reservation not found");
        }

        return new ReservationViewModel()
        {
            ReservationDate = model.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
            UserId = model.UserId,
            ReservationId = model.ReservationId,
            IsIndor = model.IsIndor,
            IsCanceled = model.IsCanceled,
            NumberOfGuests = model.NumberOfGuests,
            TableId = model.TableId,
        };
    }

    /// <summary> 
    /// Returns reservations for a specific user.
    /// </summary> 
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of reservations for the user.</returns>
    public async Task<IEnumerable<ReservationViewModel>> GetReservationsByUserIdAsync(string userId)
    {
        var reservations = await _repository.GetAllAsync();

        return reservations
            .Where(r => r.UserId == userId)
            .Where(r => r.IsCanceled == false)
            .Select(r => new ReservationViewModel
            {
                ReservationId = r.ReservationId,
                UserId = r.UserId,
                ReservationDate = r.ReservationDate.ToString(ReservationDateTimeStringFormat, CultureInfo.InvariantCulture),
                TableId = r.TableId,
                NumberOfGuests = r.NumberOfGuests,
                IsCanceled = r.IsCanceled
            })
            .ToList();
    }

    /// <summary>
    /// Updates an existing reservation asynchronously.
    /// </summary>
    /// <param name="model">The reservation view model containing the updated details of the reservation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="Exception">Thrown when the reservation is not found.</exception>
    /// <exception cref="ArgumentException">Thrown when the reservation date format is invalid.</exception>
    public async Task UpdateReservationAsync(ReservationViewModel model)
    {
        var reservation = await _repository.GetByIdAsync(model.ReservationId);

        if (reservation == null)
        {
            throw new Exception("Reservation not found");
        }

        if (!DateTime.TryParseExact(model.ReservationDate, ReservationDateTimeStringFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var reservationDate))
        {
            throw new ArgumentException("Invalid reservation date format.");
        }

        reservation.TableId = model.TableId;
        reservation.ReservationDate = reservationDate;
        reservation.IsIndor = model.IsIndor;
        reservation.IsCanceled = model.IsCanceled;
        reservation.NumberOfGuests = model.NumberOfGuests;

        await _repository.UpdateAsync(reservation);
    }

}
