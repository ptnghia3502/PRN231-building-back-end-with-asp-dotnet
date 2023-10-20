using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.ViewModels;

namespace eStoreClient.Controllers
{
    
    public class MembersController : BaseController
    {
        public MembersController() : base("http://localhost:5269/api/members")
        {
        }

        public async Task<IActionResult> Index()
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "true" && isLogin == "true")
            {
                HttpResponseMessage response = await _httpClient.GetAsync(ApiUrl);
                string strData = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Member> listMembers = JsonSerializer.Deserialize<List<Member>>(strData, options);
                return View(listMembers);
            }
            return RedirectToAction("Index", "Home");
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
                    Member memberDto = JsonSerializer.Deserialize<Member>(strData, options);

                    if (memberDto == null)
                    {
                        return NotFound();
                    }

                    ViewBag.IsAdmin = isAdmin == "true";

                    return View(memberDto);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error retrieving member details from the API.");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "true" && isLogin == "true")
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member memberDto)
        {
            if (ModelState.IsValid)
            {
                // Serialize the DTO to JSON
                var jsonContent = new StringContent(JsonSerializer.Serialize(memberDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(ApiUrl, jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating member.");
                    // var errorContent = await response.Content.ReadAsStringAsync();
                    // Console.WriteLine("Error Content: " + errorContent);
                }
            }

            return View(memberDto);
        }

        public async Task<IActionResult> Edit(int id)
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
                    Member memberDto = JsonSerializer.Deserialize<Member>(strData, options);

                    ViewBag.IsAdmin = isAdmin == "true";

                    return View(memberDto);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error retrieving member details from the API.");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MemberUpdateView memberDto)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(memberDto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{ApiUrl}/{id}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string isAdmin = HttpContext.Session.GetString("isAdmin");
                    ViewBag.IsAdmin = isAdmin == "true";
                    if (isAdmin == "true")
                    {
                        return RedirectToAction("Index");
                    } else
                    {
                        return RedirectToAction("Details", "Members");
                    }
                    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating member.");
                }
            }

            return View(memberDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string isAdmin = HttpContext.Session.GetString("isAdmin")!;
            string isLogin = HttpContext.Session.GetString("isLogin")!;
            if (isAdmin == "true" && isLogin == "true")
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string strData = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    Member memberDto = JsonSerializer.Deserialize<Member>(strData, options);

                    if (memberDto == null)
                    {
                        return NotFound();
                    }

                    return View(memberDto);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error retrieving member details from the API.");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Member deleted successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting member.";
                return RedirectToAction("Index");
            }
        }


    }
}
