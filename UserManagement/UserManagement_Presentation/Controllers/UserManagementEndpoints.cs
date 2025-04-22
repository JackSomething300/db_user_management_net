using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using UserManagement_Application.DTO_Entities;
using UserManagement_Presentation.Models;
using UserManagement_Presentation.Settings;
using Microsoft.Extensions.Options;

namespace UserManagement_Presentation.Controllers
{
    public static class UserManagementEndpoints
    {
        public static void MapUserManagementEndpoints(this IEndpointRouteBuilder route, IOptions<APIConnectionStrings> apiConnectionStrings)
        {
            var apiBaseUrl = apiConnectionStrings.Value.BaseConnection;
            var apiGroupBaseUrl = apiConnectionStrings.Value.GroupConnections;

            route.MapGet("minapi/users", async (IHttpClientFactory httpClientFactory) =>
            {
                var httpClient = httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(apiBaseUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Results.Ok(users);
            });

            route.MapGet("minapi/users/{id}", async (string id, IHttpClientFactory httpClientFactory) =>
            {
                var httpClient = httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync($"{apiBaseUrl}{id}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Results.Ok(users);
            });
        }
    }
}