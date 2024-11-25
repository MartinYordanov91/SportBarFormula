using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Core.Services.Contracts;

public interface IOrderService
{
    public Task<OrderViewModel?> GetOrderByIdAsync(int id);

    public Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();

    public Task UpdateOrderAsync(OrderViewModel orderViewModel);

    public Task<Order?> CreateOrderAsync(OrderViewModel orderViewModel);

    public Task DeleteOrderAsync(int id);
}
