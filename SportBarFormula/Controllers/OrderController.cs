using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Core.ViewModels.Order_OrderItems;

namespace SportBarFormula.Controllers;

/// <summary>
/// Order Management Controller.
/// </summary>
/// <param name="service"></param>
/// <param name="logger"></param>
[ApiController]
[Route("api/[controller]")]
public class OrderController(
    IOrderService service,
    IModelStateLoggerService logger
    ) : Controller
{

    private readonly IOrderService _service = service;
    private readonly IModelStateLoggerService _logger = logger;

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

}
