using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement_Application.DTO_Entities;

namespace UserManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var users = new List<UserDTO>
            {
                new UserDTO { Id = 1, Name = "User1" },
                new UserDTO { Id = 2, Name = "User2" }
            };


            return Ok(users);
        }

    }   
}
