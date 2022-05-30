using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class images_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CraftableItems_ImageContainers_ImageId",
                table: "CraftableItems");

            migrationBuilder.DropForeignKey(
                name: "FK_RawResources_ImageContainers_ImageId",
                table: "RawResources");

            migrationBuilder.DropTable(
                name: "ImageContainers");

            migrationBuilder.DropIndex(
                name: "IX_RawResources_ImageId",
                table: "RawResources");

            migrationBuilder.DropIndex(
                name: "IX_CraftableItems_ImageId",
                table: "CraftableItems");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "RawResources");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "CraftableItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "RawResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "CraftableItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImageContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageBytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageContainers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RawResources_ImageId",
                table: "RawResources",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CraftableItems_ImageId",
                table: "CraftableItems",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CraftableItems_ImageContainers_ImageId",
                table: "CraftableItems",
                column: "ImageId",
                principalTable: "ImageContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawResources_ImageContainers_ImageId",
                table: "RawResources",
                column: "ImageId",
                principalTable: "ImageContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
