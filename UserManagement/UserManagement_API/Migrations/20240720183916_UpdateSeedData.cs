using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Moderztors" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Level 2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Steve" });

            migrationBuilder.InsertData(
                table: "GroupPermissions",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GroupPermissions",
                keyColumns: new[] { "GroupId", "PermissionId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
