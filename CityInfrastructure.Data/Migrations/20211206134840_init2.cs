using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfrastructure.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Schedules_ScheduleId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_ScheduleId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RouteId",
                table: "Schedules",
                column: "RouteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Routes_RouteId",
                table: "Schedules",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Routes_RouteId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_RouteId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ScheduleId",
                table: "Routes",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Schedules_ScheduleId",
                table: "Routes",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id");
        }
    }
}
