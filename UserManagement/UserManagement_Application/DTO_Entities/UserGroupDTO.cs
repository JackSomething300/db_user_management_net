namespace UserManagement_Application.DTO_Entities
{
    public class UserGroupDTO
    {
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int GroupId { get; set; }
        public GroupDTO Group { get; set; }
    }
}
