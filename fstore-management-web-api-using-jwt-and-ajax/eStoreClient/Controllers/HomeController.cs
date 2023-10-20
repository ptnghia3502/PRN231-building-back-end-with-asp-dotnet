using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using eStoreClient.Models;
using Microsoft.AspNetCore.Mvc;
using Services.ViewModels;
using Microsoft.Extensions.Configuration;
using Humanizer.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Repositories.Models;

namespace eStoreClient.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration) : base("http://localhost:5269/api/authentications")
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginView loginDto)
        {
            HttpContext.Session.SetString("isLogin", "");
            HttpContext.Session.SetString("isAdmin", "");

            if (ModelState.IsValid)
            {
                if (loginDto.Email == _configuration.GetSection("AdminCredentials")["Email"] &&
                    loginDto.Password == _configuration.GetSection("AdminCredentials")["Password"])
                {
                    var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        string strData = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(strData, options);

                        if (apiResponse != null)
                        {
                            HttpContext.Session.SetString("token", apiResponse.Token);
                        }

                        else
                        {
                            TempData["error"] = "Wrong email or password";
                        }
                    }
                    else
                    {
                        TempData["error"] = "API request failed. Please try again.";
                    }



                    HttpContext.Session.SetString("isLogin", "true");
                    HttpContext.Session.SetString("isAdmin", "true");
                    return RedirectToAction("Admin", "Home");
                }
                else
                {
                    var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        string strData = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(strData, options);

                        if (apiResponse != null)
                        {
                            // Store the JWT token in the session
                            HttpContext.Session.SetString("isLogin", "true");
                            HttpContext.Session.SetString("isAdmin", "false");
                            HttpContext.Session.SetString("token", apiResponse.Token);

                            HttpContext.Session.SetString("currentUserId", apiResponse.Member.MemberId.ToString());

                            return RedirectToAction("Member", "Home");
                        }

                        else
                        {
                            TempData["error"] = "Wrong email or password";
                        }
                    }
                    else
                    {
                        TempData["error"] = "API request failed. Please try again.";
                    }
                }
            }

            return View();
        }


        public IActionResult Admin()
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "true" && isLogin == "true")
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Member()
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "false" && isLogin == "true")
            {
                string currentUserId = HttpContext.Session.GetString("currentUserId")!;
                ViewBag.CurrentUserId = currentUserId;
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear all sessions
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}