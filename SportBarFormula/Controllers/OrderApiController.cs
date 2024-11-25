using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Controllers
{
    /// <summary>
    /// Order Management API Controller.
    /// Provides endpoints for retrieving order information.
    /// </summary>
    /// <param name="service">The order service instance.</param>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderApiController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderApiController(IOrderService service)
        {
            _service = service;
        }

        //-------------------------------------------------------------------------------------------------------> GetOrderById
        /// <summary>
        /// Retrieves an order by its ID and returns the corresponding OrderViewModel.
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        /// <returns>
        /// An ActionResult containing the OrderViewModel if the order is found,
        /// or a NotFound result if the order is not found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrderById(int id)
        {
            var orderViewlModel = await _service.GetOrderByIdAsync(id);

            if (orderViewlModel == null)
            {
                return NotFound();
            }

            return Ok(orderViewlModel);
        }

        //-------------------------------------------------------------------------------------------------------> GetAllOrders 
        /// <summary> 
        /// Retrieves all orders and returns the corresponding list of OrderViewModel. 
        /// </summary> 
        /// <returns> 
        /// An ActionResult containing the list of OrderViewModel.
        /// </returns> 
        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var allOrders = await _service.GetAllOrdersAsync();
            return Ok(allOrders);
        }

        //-------------------------------------------------------------------------------------------------------> UpdateOrder
        /// <summary>
        /// Updates an existing order with the provided OrderViewModel.
        /// </summary>
        /// <param name="id">The ID of the order to be updated.</param>
        /// <param name="orderViewModel">The OrderViewModel containing updated order details.</param>
        /// <returns>
        /// An ActionResult indicating the result of the update operation.
        /// Returns BadRequest if the ID in the URL does not match the ID in the OrderViewModel.
        /// Returns NoContent if the update operation is successful.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderViewModel orderViewModel)
        {
            if (id != orderViewModel.OrderId)
            {
                return BadRequest();
            }

            await _service.UpdateOrderAsync(orderViewModel);

            return NoContent();
        }

        //-------------------------------------------------------------------------------------------------------------------> CreateOrder
        /// <summary>
        /// Creates a new order using the provided OrderViewModel.
        /// </summary>
        /// <param name="orderViewModel">The OrderViewModel containing details of the order to be created.</param>
        /// <returns>
        /// An ActionResult indicating the result of the create operation.
        /// Returns BadRequest if the order creation fails.
        /// Returns CreatedAtAction with the newly created order's details if successful.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderViewModel orderViewModel)
        {
            var order = await _service.CreateOrderAsync(orderViewModel);

            if (order == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, orderViewModel);
        }

        //-----------------------------------------------------------------------------------------------------------------> DeleteOrder
        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        /// <returns>An ActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _service.DeleteOrderAsync(id);

            return NoContent();
        }

    }
}
