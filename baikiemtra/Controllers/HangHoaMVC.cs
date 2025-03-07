using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HangHoaManagement.Models;

namespace HangHoaMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public HangHoaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiUrl = "https://localhost:7257/api/HangHoa"; // Địa chỉ Web API
        }

        // GET: HangHoa
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var hangHoaList = await response.Content.ReadFromJsonAsync<List<hang_hoa>>();
                return View(hangHoaList);
            }

            return View(new List<hang_hoa>());
        }

        // GET: HangHoa/Details/ABC123456
        public async Task<IActionResult> Details(string id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<hang_hoa>();
                return View(hangHoa);
            }

            return NotFound();
        }

        // GET: HangHoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ma_hanghoa,ten_hanghoa,so_luong,ghi_chu")] hang_hoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(hangHoa), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Edit/ABC123456
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<hang_hoa>();
                return View(hangHoa);
            }

            return NotFound();
        }

        // POST: HangHoa/Edit/ABC123456
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ma_hanghoa,ten_hanghoa,so_luong,ghi_chu")] hang_hoa hangHoa)
        {
            if (id != hangHoa.ma_hanghoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(hangHoa), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Delete/ABC123456
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<hang_hoa>();
                return View(hangHoa);
            }

            return NotFound();
        }

        // POST: HangHoa/Delete/ABC123456
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}