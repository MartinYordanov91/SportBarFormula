using Microsoft.AspNetCore.Mvc.Rendering;
using SportBarFormula.Core.ViewModels.Reservation;

namespace SportBarFormula.Core.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationViewModel>> GetAllReservationsAsync();

    Task<IEnumerable<ReservationViewModel>> GetReservationsByUserIdAsync(string userId);

    Task AddReservationAsync(ReservationViewModel model);

    Task<ReservationViewModel> GetReservationByIdAsync(int id);

    Task UpdateReservationAsync(ReservationViewModel model);
}
