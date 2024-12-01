using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportBarFormula.Core.Services.Contracts;

namespace SportBarFormula.Controllers;

/// <summary>
/// Controller for managing orders.
/// </summary>
public class OrderController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IOrderService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderController"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="service">The order service.</param>
    public OrderController(UserManager<IdentityUser> userManager, IOrderService service)
    {
        _userManager = userManager;
        _service = service;
    }

    //--------------------------------------------------------------------------------------------------------------------------------------> AddToCart
    /// <summary>
    /// Adds an item to the cart.
    /// </summary>
    /// <param name="menuItemId">The ID of the menu item.</param>
    /// <param name="quantity">The quantity of the item. Default is 1.</param>
    /// <returns>The result of the action.</returns>
    public async Task<IActionResult> AddToCart(int menuItemId, int quantity = 1)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var userOrder = await _service.GetUserDraftOrder(user);

        await _service.AddItemToCartAsync(menuItemId, quantity, userOrder);

        return RedirectToAction(nameof(MenuController.Details), nameof(MenuController).Replace("Controller", ""), new { id = menuItemId });
    }

    //--------------------------------------------------------------------------------------------------------------------------------------> MyCart
    /// <summary>
    /// Displays the user's cart.
    /// </summary>
    /// <returns>The cart view.</returns>
    public async Task<IActionResult> MyCart()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var userOrder = await _service.GetUserDraftOrder(user);

        return View(userOrder);
    }

    //--------------------------------------------------------------------------------------------------------------------------------------> Checkout
    /// <summary>
    /// Completes the checkout process.
    /// </summary>
    /// <returns>The result of the checkout action.</returns>
    public async Task<IActionResult> Checkout()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var userOrder = await _service.GetUserDraftOrder(user);

        await _service.CompletedOrderAsync(userOrder);

        return RedirectToAction(nameof(HomeController.Index));
    }
}
