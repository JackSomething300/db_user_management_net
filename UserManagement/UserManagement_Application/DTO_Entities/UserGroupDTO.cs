using System.Text.Json.Serialization;

namespace UserManagement_Application.DTO_Entities
{
    public class UserGroupDTO
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public UserDTO? User { get; set; }
        public int GroupId { get; set; }
        [JsonIgnore]
        public GroupDTO? Group { get; set; }
    }
}
