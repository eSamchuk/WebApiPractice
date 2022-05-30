using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfrastructure.Data.Migrations
{
    public partial class decimalDepth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Depth",
                table: "SubwayStations",
                type: "decimal(12,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Depth",
                table: "SubwayStations",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,0)");
        }
    }
}
