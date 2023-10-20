using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.ViewModels;
using static NuGet.Packaging.PackagingConstants;

namespace eStoreClient.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController() : base("http://localhost:5269/api/orders")
        {
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
            return View(listOrders);
        }

        public async Task<IActionResult> Details(int id)
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isLogin == "true")
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    Order ordertDto = JsonSerializer.Deserialize<Order>(strData, options);

                    if (ordertDto == null)
                    {
                        return NotFound();
                    }

                    ViewBag.IsAdmin = isAdmin == "true";

                    return View(ordertDto);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error retrieving product details from the API.");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Order orderDto = JsonSerializer.Deserialize<Order>(strData, options);
                return View(orderDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving order details from the API.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderUpdateView orderDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(orderDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{ApiUrl}/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating order.");
                }
            }

            return View(orderDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Order orderDto = JsonSerializer.Deserialize<Order>(strData, options);

                if (orderDto == null)
                {
                    return NotFound();
                }

                return View(orderDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving order details from the API.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Order deleted successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting Order.";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> OrdersOfMember()
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "false" && isLogin == "true")
            {
                string currentUserId = HttpContext.Session.GetString("currentUserId")!;
                HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{currentUserId}/orders-detail");

                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Order> listOrders = JsonSerializer.Deserialize<List<Order>>(strData, options);
                return View(listOrders);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
