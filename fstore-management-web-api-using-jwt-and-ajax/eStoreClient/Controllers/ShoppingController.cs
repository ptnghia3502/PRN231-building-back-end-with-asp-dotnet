using System.Text.Json;
using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Models;
using Microsoft.AspNetCore.Http;
using Services.ViewModels;
using System.Text;
using System.Net.Http.Headers;

namespace eStoreClient.Controllers
{
    public class ShoppingController : BaseController
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingController(ShoppingCart shoppingCart) : base("http://localhost:5269/api/products")
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            string tokenString = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);

            ViewData["JwtToken"] = tokenString;

            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }

        public IActionResult Cart()
        {
            return View(_shoppingCart);
        }

        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            string tokenString = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Product productToAdd = JsonSerializer.Deserialize<Product>(strData, options);

                if (productToAdd != null)
                {
                    _shoppingCart.AddToCart(productToAdd, quantity);
                    return RedirectToAction("Cart");
                }
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            _shoppingCart.UpdateQuantity(productId, quantity);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            _shoppingCart.RemoveFromCart(productId);
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> CreateOrder()
        {
            // Get the list of items in the cart
            var cartItems = _shoppingCart.CartItems;
            string currentUserId = HttpContext.Session.GetString("currentUserId")!;

            Random random = new Random();
            var orderDetails = cartItems.Select(cartItem => new OrderDetailView
            {
                ProductId = cartItem.Product.ProductId,
                UnitPrice = cartItem.Product.UnitPrice,
                Quantity = cartItem.Quantity,
                Discount = random.Next(1, 101)
            }).ToList();

            OrderCreateView orderDto = new OrderCreateView
            {
                OrderId = random.Next(1, 10001),
                OrderDate = DateTime.Now,
                RequiredDate = DateTime.Now,
                MemberId = Int32.Parse(currentUserId),
                Freight = random.Next(10000, 90001),
                OrderDetails = orderDetails,
            };

            // Serialize the DTO to JSON
            var jsonContent = new StringContent(JsonSerializer.Serialize(orderDto), Encoding.UTF8, "application/json");

            string tokenString = HttpContext.Session.GetString("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5269/api/orders", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                _shoppingCart.ClearCart();
                return RedirectToAction("OrdersOfMember", "Orders");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error creating order.");
            }

            return RedirectToAction("Error", "Shared");
        }

    }
}
