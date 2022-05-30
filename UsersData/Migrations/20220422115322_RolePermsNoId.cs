using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersData.Migrations
{
    public partial class RolePermsNoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolePermission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RolePermission",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
