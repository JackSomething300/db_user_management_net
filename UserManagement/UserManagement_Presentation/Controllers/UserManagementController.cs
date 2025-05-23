﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using UserManagement_Application.DTO_Entities;
using UserManagement_Presentation.Models;
using UserManagement_Presentation.Settings;
using Microsoft.Extensions.Options;

namespace UserManagement_Presentation.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "";
        private readonly string _apiGroupBaseUrl = "";
        private readonly ILogger<UserManagementController> _logger;
        private readonly APIConnectionStrings _apiConnectionStrings;

        public UserManagementController(IHttpClientFactory httpClientFactory, ILogger<UserManagementController> logger, IOptions<APIConnectionStrings> apiConnectionStrings)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiBaseUrl = apiConnectionStrings.Value.BaseConnection;
            _apiGroupBaseUrl = apiConnectionStrings.Value.GroupConnections;
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
            foreach (var groupId in viewModel.GroupIds)
            {
                var userGroupDto = new UserGroupDTO
                {
                    GroupId = groupId
                };
                viewModel.User.UserGroups.Add(userGroupDto);
            }

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
            var response = await httpClient.GetAsync($"{_apiBaseUrl}{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            UserGroupViewModel userGroupViewModel = new UserGroupViewModel();
            userGroupViewModel.User = user;

            var groupResponse = await httpClient.GetAsync($"{_apiGroupBaseUrl}");
            groupResponse.EnsureSuccessStatusCode();
            var groupReponseBody = await groupResponse.Content.ReadAsStringAsync();
            var groups = JsonSerializer.Deserialize<IEnumerable<GroupDTO>>(groupReponseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            userGroupViewModel.AllGroups = groups;  

            userGroupViewModel.SelectedGroupIds = user.UserGroups.Select(ug => ug.GroupId).ToList();

            if (user == null)
            {
                return NotFound();
            }
            return View(userGroupViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.User.UserGroups.Clear();

                foreach (var groupId in viewModel.SelectedGroupIds)
                {
                    var userGroupDto = new UserGroupDTO
                    {
                        GroupId = groupId,
                        UserId = viewModel.User.Id
                    };
                    viewModel.User.UserGroups.Add(userGroupDto);
                }

                var httpClient = _httpClientFactory.CreateClient();
                var userJson = JsonSerializer.Serialize(viewModel.User);
                var content = new StringContent(userJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"{_apiBaseUrl}{viewModel.User.Id}", content);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 1)
            {
                //cannot delete admin user
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{_apiBaseUrl}{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error"); 
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"{_apiBaseUrl}{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}