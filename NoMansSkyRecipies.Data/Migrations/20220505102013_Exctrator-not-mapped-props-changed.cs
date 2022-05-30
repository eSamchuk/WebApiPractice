using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class Exctratornotmappedpropschanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiningOutposts_RawResourceTypes_ResourceTypeId",
                table: "MiningOutposts");

            migrationBuilder.DropColumn(
                name: "ExtractorCount",
                table: "MiningOutposts");

            migrationBuilder.AlterColumn<string>(
                name: "Class",
                table: "HotspotClasses",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SinglePlantAmount = table.Column<int>(nullable: false),
                    ResourceId = table.Column<int>(nullable: false),
                    Climate = table.Column<string>(nullable: true),
                    GrowTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_RawResources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "RawResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biodome",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biodome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biodome_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "HotspotClasses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Class",
                value: "B");

            migrationBuilder.UpdateData(
                table: "HotspotClasses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Class",
                value: "C");

            migrationBuilder.CreateIndex(
                name: "IX_Biodome_PlantId",
                table: "Biodome",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_ResourceId",
                table: "Plants",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MiningOutposts_RawResources_ResourceTypeId",
                table: "MiningOutposts",
                column: "ResourceTypeId",
                principalTable: "RawResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MiningOutposts_RawResources_ResourceTypeId",
                table: "MiningOutposts");

            migrationBuilder.DropTable(
                name: "Biodome");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.AddColumn<int>(
                name: "ExtractorCount",
                table: "MiningOutposts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Class",
                table: "HotspotClasses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1);

            migrationBuilder.UpdateData(
                table: "HotspotClasses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Class",
                value: "A");

            migrationBuilder.UpdateData(
                table: "HotspotClasses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Class",
                value: "A");

            migrationBuilder.AddForeignKey(
                name: "FK_MiningOutposts_RawResourceTypes_ResourceTypeId",
                table: "MiningOutposts",
                column: "ResourceTypeId",
                principalTable: "RawResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
