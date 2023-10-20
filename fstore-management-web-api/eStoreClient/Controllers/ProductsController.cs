using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.Models;
using Services.ViewModels;
using System.Text;
using System.Text.Json;

namespace eStoreClient.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController() : base("http://localhost:5269/api/products")
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
            List<Product> listProducts = JsonSerializer.Deserialize<List<Product>>(strData, options);
            return View(listProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Product productDto = JsonSerializer.Deserialize<Product>(strData, options);

                if (productDto == null)
                {
                    return NotFound();
                }

                return View(productDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving product details from the API.");
                return RedirectToAction("Index");
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5269/api/categories");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Category> listCategories = JsonSerializer.Deserialize<List<Category>>(strData, options);
            return listCategories;
        }

        public async Task<IActionResult> Create()
        {
            List<Category> categories = await GetCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product productDto)
        {
            if (ModelState.IsValid)
            {
                // Serialize the DTO to JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating member.");
                }
            }

            return View(productDto);
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
                Product productDto = JsonSerializer.Deserialize<Product>(strData, options);

                List<Category> categories = await GetCategories();
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

                return View(productDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving product details from the API.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductUpdateView productDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(productDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{ApiUrl}/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating product.");
                }
            }

            return View(productDto);
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
                Product productDto = JsonSerializer.Deserialize<Product>(strData, options);

                if (productDto == null)
                {
                    return NotFound();
                }

                return View(productDto);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error retrieving product details from the API.");
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
                TempData["SuccessMessage"] = "Product deleted successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting product.";
                return RedirectToAction("Index");
            }
        }
    }
}
