using Microsoft.AspNetCore.Mvc;
using st10293982CLD6212POE1.Models;

namespace st10293982CLD6212POE1.Controllers
{
    public class CartController : Controller
    {
        private readonly ISession _session;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        // Load the cart from the session
        private Cart GetCart()
        {
            var cart = _session.GetObjectFromJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        // Save the cart to the session
        private void SaveCart(Cart cart)
        {
            _session.SetObjectAsJson("Cart", cart);
        }

        // Add an item to the cart
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price, int quantity)
        {
            var cart = GetCart();

            var cartItem = new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };

            cart.AddItem(cartItem);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        // Remove an item from the cart
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            cart.RemoveItem(productId);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        // View the cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
    }
}
