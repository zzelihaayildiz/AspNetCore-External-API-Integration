using Microsoft.AspNetCore.Mvc;
using ApiDataProject.Models;
using System.Net.Http.Json;

namespace ApiDataProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UsersController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var baseUrl = _configuration["ApiSettings:BaseUrl"];

                var users = await _httpClient.GetFromJsonAsync<List<User>>(
                    $"{baseUrl}/users");

                if (users == null)
                {
                    ViewBag.ErrorMessage = "Kullanıcı verisi bulunamadı.";
                    return View(new List<User>());
                }

                return View(users);
            }
            catch (HttpRequestException)
            {
                ViewBag.ErrorMessage = "API bağlantısı başarısız oldu.";
                return View(new List<User>());
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Beklenmeyen bir hata oluştu.";
                return View(new List<User>());
            }
        }
    }
}