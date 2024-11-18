using SportBarFormula.Core.ViewModels.Reservation;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Core.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationViewModel>> GetAllReservationsAsync();
}
