namespace UserManagement_Core.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<UserGroup> UserGroups { get; set; }
}
