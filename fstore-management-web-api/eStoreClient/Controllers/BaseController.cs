using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace eStoreClient.Controllers
{
    public class BaseController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected string ApiUrl { get; }

        public BaseController(string apiUrl)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            ApiUrl = apiUrl;
        }
    }
}
