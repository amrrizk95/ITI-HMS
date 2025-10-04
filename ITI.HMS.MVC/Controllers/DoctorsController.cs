using Microsoft.AspNetCore.Mvc;
using ITI.HMS.MVC.Models;
using System.Text.Json;
using System.Net.Http.Headers;

namespace ITI.HMS.MVC.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IHttpClientFactory httpClientFactory, ILogger<DoctorsController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("HMSAPI");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Add authorization header if token exists
                var token = HttpContext.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.GetAsync("api/doctors");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var doctors = JsonSerializer.Deserialize<List<Doctor>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    ViewBag.Success = "✅ MVC Success! Server-to-server call works without CORS configuration.";
                    ViewBag.ApiCall = $"GET {_httpClient.BaseAddress}api/doctors";
                    ViewBag.Explanation = "This works because MVC makes server-side HTTP calls, not browser calls.";

                    return View(doctors ?? new List<Doctor>());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewBag.Error = "⚠️ Authentication required. Please login first.";
                    ViewBag.LoginLink = true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"API Error: {response.StatusCode} - {errorContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error fetching doctors");
                ViewBag.Error = $"Network error: {ex.Message}";
                ViewBag.TechnicalNote = "If you see connection errors, ensure the HMS API is running on https://localhost:5000";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching doctors");
                ViewBag.Error = $"Unexpected error: {ex.Message}";
            }

            return View(new List<Doctor>());
        }
    }
}