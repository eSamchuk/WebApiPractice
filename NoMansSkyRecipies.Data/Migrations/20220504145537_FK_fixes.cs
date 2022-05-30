using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class FK_fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectroMagneticPlants_HotspotClasses_HotspotClassId1",
                table: "ElectroMagneticPlants");

            migrationBuilder.DropForeignKey(
                name: "FK_MiningOutposts_HotspotClasses_HotspotClassId1",
                table: "MiningOutposts");

            migrationBuilder.DropIndex(
                name: "IX_MiningOutposts_HotspotClassId1",
                table: "MiningOutposts");

            migrationBuilder.DropIndex(
                name: "IX_ElectroMagneticPlants_HotspotClassId1",
                table: "ElectroMagneticPlants");

            migrationBuilder.DropColumn(
                name: "HotspotClassId1",
                table: "MiningOutposts");

            migrationBuilder.DropColumn(
                name: "HotspotClassId1",
                table: "ElectroMagneticPlants");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotspotClassId1",
                table: "MiningOutposts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotspotClassId1",
                table: "ElectroMagneticPlants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MiningOutposts_HotspotClassId1",
                table: "MiningOutposts",
                column: "HotspotClassId1");

            migrationBuilder.CreateIndex(
                name: "IX_ElectroMagneticPlants_HotspotClassId1",
                table: "ElectroMagneticPlants",
                column: "HotspotClassId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectroMagneticPlants_HotspotClasses_HotspotClassId1",
                table: "ElectroMagneticPlants",
                column: "HotspotClassId1",
                principalTable: "HotspotClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MiningOutposts_HotspotClasses_HotspotClassId1",
                table: "MiningOutposts",
                column: "HotspotClassId1",
                principalTable: "HotspotClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
