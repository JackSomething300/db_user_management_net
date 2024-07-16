namespace UserManagement_Application.DTO_Entities
{
    public class GroupPermissionDTO
    {
        public int GroupId { get; set; }
        public GroupDTO Group { get; set; }
        public int PermissionId { get; set; }
        public PermissionDTO Permission { get; set; }
    }
}
