using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement_Application.DTO_Entities;
using UserManagement_Application.Interfaces;

namespace UserManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupManagementController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupManagementController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetGroup(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetUsers()
        {
            var groups = await _groupService.GetAllGroups();
            return Ok(groups);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] GroupDTO groupDTO)
        {
            if (groupDTO == null || string.IsNullOrEmpty(groupDTO.Name))
            {
                return BadRequest("Group cannot be null");
            }

            await _groupService.AddGroupAsync(groupDTO);
            //does lookup for newly added group and returns it
            return CreatedAtAction(nameof(GetGroup), new { id = groupDTO.Id }, groupDTO);
        }
    }
}
