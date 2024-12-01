using Microsoft.AspNetCore.Identity;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Core.Services.Contracts;

public interface IOrderService
{
    public Task<OrderViewModel> GetUserDraftOrder(IdentityUser user);

    public Task AddOrderAsync(OrderViewModel order);

    public Task AddItemToCartAsync(int menuItemId, int quantity, OrderViewModel order);

    public Task AddOrderItemAsync(OrderItemViewModel orderitem, int orderId);

    public Task CompletedOrderAsync(OrderViewModel order);
}
