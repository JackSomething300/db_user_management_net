using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using UserManagement_Application.DTO_Entities;
using UserManagement_Presentation.Models;

namespace UserManagement_Presentation.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "https://localhost:7046/api/UserManagement/";
        private readonly string _apiGroupBaseUrl = "https://localhost:7046/api/GroupManagement/";

        public UserManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiBaseUrl}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(users);
        }


        public async Task<IActionResult> Create()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiGroupBaseUrl}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var groups = JsonSerializer.Deserialize<IEnumerable<GroupDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.Groups = groups;
            return View(new UserGroupViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserGroupViewModel viewModel)
        {
            var userGroupDto = new UserGroupDTO
            {
                GroupId = viewModel.GroupId
            };
            viewModel.User.UserGroups = new List<UserGroupDTO> { userGroupDto };

            var httpClient = _httpClientFactory.CreateClient();
            var userJson = JsonSerializer.Serialize(viewModel.User);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{_apiBaseUrl}", content);
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiBaseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();
                var userJson = JsonSerializer.Serialize(userDto);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{_apiBaseUrl}/{userDto.Id}", content);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(userDto);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiBaseUrl}/user/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{_apiBaseUrl}/user/{id}");
            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }
    }
}