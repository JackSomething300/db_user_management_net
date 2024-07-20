using Microsoft.EntityFrameworkCore;
using UserManagement_Core.Entities;

namespace UserManagement_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
                
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupPermission> GroupPermissions { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });
            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            // Seed data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin" }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Admins" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "Level 1" }
            );

            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { UserId = 1, GroupId = 1 }
            );

            modelBuilder.Entity<GroupPermission>().HasData(
                new GroupPermission { GroupId = 1, PermissionId = 1 }
            );
        }
    }
}
