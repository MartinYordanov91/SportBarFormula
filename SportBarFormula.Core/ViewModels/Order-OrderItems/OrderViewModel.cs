using SportBarFormula.Infrastructure.Data.Enums;

namespace SportBarFormula.Core.ViewModels.Order_OrderItems;

/// <summary>
/// Represents an order made by a user.
/// </summary>
public class OrderViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderViewModel"/> class.
    /// </summary>
    public OrderViewModel()
    {
        this.OrderItems = new List<OrderItemViewModel>();
    }

    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who placed the order.
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date of the order in string format.
    /// </summary>
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the total amount of the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the status of the order.
    /// 
    /// The possible values are:
    /// - <see cref="OrderStatus.Draft"/>: The order has been created but not yet completed.
    /// - <see cref="OrderStatus.Completed"/>: The order has been completed and processed.
    /// - <see cref="OrderStatus.Canceled"/>: The order has been canceled.
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the list of items in the order.
    /// </summary>
    public List<OrderItemViewModel> OrderItems { get; set; }
}
