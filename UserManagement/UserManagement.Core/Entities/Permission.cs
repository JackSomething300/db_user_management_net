namespace UserManagement_Core.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
