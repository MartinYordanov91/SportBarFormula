using SportBarFormula.Core.ViewModels.Reservation;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using System.Threading.Tasks;

namespace SportBarFormula.Core.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<ReservationViewModel>> GetAllReservationsAsync();

    Task<IEnumerable<ReservationViewModel>> GetReservationsByUserIdAsync(string userId);
}
