using Microsoft.AspNetCore.Mvc;
using ITI.HMS.MVC.Models;
using System.Text.Json;
using System.Text;

namespace ITI.HMS.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IHttpClientFactory httpClientFactory, ILogger<AuthController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("HMSAPI");
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            try
            {
                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (authResponse != null)
                    {
                        // Store auth info in session
                        HttpContext.Session.SetString("Token", authResponse.Token);
                        HttpContext.Session.SetString("Username", authResponse.Username);
                        HttpContext.Session.SetString("Role", authResponse.Role.ToString());

                        ViewBag.Success = "Login successful! ✅ No CORS issues with MVC server-side calls.";
                        ViewBag.AuthResponse = authResponse;
                        return View(loginRequest);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Login failed: {response.StatusCode} - {errorContent}";
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP error during login");
                ViewBag.Error = $"Network error: {ex.Message}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during login");
                ViewBag.Error = $"Unexpected error: {ex.Message}";
            }

            return View(loginRequest);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}