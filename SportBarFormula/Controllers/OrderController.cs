﻿using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.ViewModels.Order_OrderItems;
using System.Net.Http;

namespace SportBarFormula.Controllers
{
    /// <summary>
    /// Controller for managing orders.
    /// </summary>
    /// <param name="clientFactory">The HttpClientFactory instance.</param>
    [Route("[controller]")]
    public class OrderController(IHttpClientFactory clientFactory) : Controller
    {
        private readonly IHttpClientFactory _clientFactory = clientFactory;

        //--------------------------------------------------------------------------------------------------------> Index
        /// <summary>
        /// Renders the list of all orders.
        /// </summary>
        /// <returns>
        /// An IActionResult that renders the ListOrders view with the orders data.
        /// Returns a NoContent status if the response is successful but contains no data.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7040/api/orderapi/getallorders");

            if (!response.IsSuccessStatusCode)
            {
                return NoContent();
            }

            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderViewModel>>();

            return View("ListOrders", orders);
        }
        //---------------------------------------------------------------------------------------------------------> Create
        /// <summary>
        /// Creates a new order using the provided OrderViewModel.
        /// </summary>
        /// <param name="orderViewModel">The OrderViewModel containing details of the order to be created.</param>
        /// <returns>
        /// An ActionResult indicating the result of the create operation.
        /// Returns BadRequest if the creation fails.
        /// Redirects to the Index action upon successful creation.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.PostAsJsonAsync("https://localhost:7040/api/orderapi", orderViewModel);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
