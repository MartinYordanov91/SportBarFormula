using SportBarFormula.Core.ViewModels.Order_OrderItems;

namespace SportBarFormula.Core.Services.Contracts;

public interface IOrderService
{
    public Task<OrderViewModel?> GetOrderByIdAsync(int id);

    public Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
}
