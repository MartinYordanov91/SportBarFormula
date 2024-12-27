/**
 * Asynchronously fetches the cart item count from the server and updates the cart count display.
 * @async
 * @function updateCartCount
 */
async function updateCartCount() {
    try {
        // Fetch the cart item count from the server
        const response = await fetch('https://localhost:7040/Order/GetCartItemCount');
        const cartItemCount = await response.json();

        // Get the cart count element by its ID
        let cartCountElement = document.getElementById('cart-count');

        // Update the text content with the cart item count
        cartCountElement.textContent = cartItemCount;

        // Display the cart count element if there are items in the cart, hide it otherwise
        if (cartItemCount > 0) {
            cartCountElement.style.display = 'inline-block';
        } else {
            cartCountElement.style.display = 'none';
        }
    } catch (error) {
        console.error('Error fetching cart item count:', error);
    }
}

/**
 * Adds an event listener to update the cart count when the DOM content is loaded.
 */
document.addEventListener('DOMContentLoaded', updateCartCount);
