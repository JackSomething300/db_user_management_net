namespace UserManagement_Application.DTO_Entities
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupPermissionDTO> GroupPermissions { get; set; }
    }
}
