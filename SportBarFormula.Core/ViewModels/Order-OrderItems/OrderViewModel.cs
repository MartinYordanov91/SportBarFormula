namespace SportBarFormula.Core.ViewModels.Order_OrderItems
{
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
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the order in string format.
        /// </summary>
        public string OrderDate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the list of items in the order.
        /// </summary>
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
