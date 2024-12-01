namespace SportBarFormula.Core.ViewModels.Order_OrderItems;

/// <summary>
/// Represents an item in an order.
/// </summary>
public class OrderItemViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier of the order item.
    /// </summary>
    public int OrderItemId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the menu item.
    /// </summary>
    public int MenuItemId { get; set; }

    /// <summary>
    /// Gets or sets the name of the menu item.
    /// </summary>
    public string MenuItemName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the menu item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the price of the menu item at the time of the order.
    /// </summary>
    public decimal Price { get; set; }
}
