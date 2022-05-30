using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageContainers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageBytes = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageContainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RawResourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CraftableItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CraftableItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CraftableItems_ImageContainers_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RawResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Value = table.Column<int>(nullable: false),
                    RawResourceTypeId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RawResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RawResources_ImageContainers_ImageId",
                        column: x => x.ImageId,
                        principalTable: "ImageContainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RawResources_RawResourceTypes_RawResourceTypeId",
                        column: x => x.RawResourceTypeId,
                        principalTable: "RawResourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultingItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipies_CraftableItems_ResultingItemId",
                        column: x => x.ResultingItemId,
                        principalTable: "CraftableItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NeededResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipieId = table.Column<int>(nullable: false),
                    RawResourceId = table.Column<int>(nullable: true),
                    CraftableItemId = table.Column<int>(nullable: true),
                    NeededAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NeededResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NeededResources_CraftableItems_CraftableItemId",
                        column: x => x.CraftableItemId,
                        principalTable: "CraftableItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NeededResources_RawResources_RawResourceId",
                        column: x => x.RawResourceId,
                        principalTable: "RawResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NeededResources_Recipies_RecipieId",
                        column: x => x.RecipieId,
                        principalTable: "Recipies",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CraftableItems",
                columns: new[] { "Id", "ImageId", "Name", "Value" },
                values: new object[,]
                {
                    { 1, null, "Acid", 188000 },
                    { 31, null, "Iridesite", 150000 },
                    { 30, null, "Unstable Gel", 50000 },
                    { 29, null, "Thermic Condensate", 50000 },
                    { 28, null, "Superconductor", 1500000 },
                    { 27, null, "Stasis Device", 15600000 },
                    { 26, null, "Semiconductor", 400000 },
                    { 25, null, "Quantum Processor", 5200000 },
                    { 24, null, "Portable Reactor", 4200000 },
                    { 23, null, "Poly Fibre", 130000 },
                    { 22, null, "Organic Catalyst", 320000 },
                    { 21, null, "Nitrogen Salt", 50000 },
                    { 20, null, "Magno-Gold", 25000 },
                    { 19, null, "Lubricant", 110000 },
                    { 17, null, "Liquid Explosive", 800500 },
                    { 18, null, "Living Glass", 566000 },
                    { 15, null, "Hot Ice", 320000 },
                    { 2, null, "Aronium", 25000 },
                    { 3, null, "Circut Board", 916250 },
                    { 4, null, "Cryo Pump", 1500000 },
                    { 5, null, "Cryogenic Chamber", 3800000 },
                    { 6, null, "Dirty Bronze", 25000 },
                    { 16, null, "Lemmium", 25000 },
                    { 8, null, "Fusion Accelerant", 1500000 },
                    { 7, null, "Enriched Carbon", 50000 },
                    { 10, null, "Glass", 200 },
                    { 11, null, "Goedesite", 150000 },
                    { 12, null, "Grantine", 25000 },
                    { 13, null, "Heat Capacitor", 188000 },
                    { 14, null, "Herox", 25000 },
                    { 9, null, "Fusion Ignitor", 15600000 }
                });

            migrationBuilder.InsertData(
                table: "RawResourceTypes",
                columns: new[] { "Id", "ResourceTypeName" },
                values: new object[,]
                {
                    { 3, "Atospheric gas" },
                    { 1, "Mined mineral" },
                    { 2, "Harvested plant" },
                    { 4, "Compressed mineral" }
                });

            migrationBuilder.InsertData(
                table: "RawResources",
                columns: new[] { "Id", "ImageId", "Name", "RawResourceTypeId", "Value" },
                values: new object[,]
                {
                    { 14, null, "Pure Ferrite", 4, 28 },
                    { 1, null, "Ammonia", 1, 62 },
                    { 4, null, "Dioxite", 1, 62 },
                    { 12, null, "Paraffinium", 1, 62 },
                    { 13, null, "Phosphorus", 1, 62 },
                    { 15, null, "Pyrite", 1, 62 },
                    { 20, null, "Uranium", 1, 62 },
                    { 5, null, "Faecium", 2, 30 },
                    { 6, null, "Frost Crystal", 2, 12 },
                    { 2, null, "Cactus Flesh", 2, 28 },
                    { 8, null, "GammaRoot", 2, 16 },
                    { 10, null, "Mordite", 2, 40 },
                    { 17, null, "Solanium", 2, 70 },
                    { 18, null, "Star Bulb", 2, 32 },
                    { 11, null, "Nitrogen", 3, 20 },
                    { 16, null, "Radon", 3, 20 },
                    { 19, null, "Sulphurine", 3, 20 },
                    { 3, null, "Condenced Carbon", 4, 24 },
                    { 7, null, "Fungal Mold", 2, 16 },
                    { 9, null, "Ionized Cobalt", 4, 401 }
                });

            migrationBuilder.InsertData(
                table: "Recipies",
                columns: new[] { "Id", "ResultingItemId" },
                values: new object[,]
                {
                    { 27, 27 },
                    { 31, 31 },
                    { 30, 30 },
                    { 29, 29 },
                    { 28, 28 },
                    { 26, 26 },
                    { 24, 24 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 },
                    { 11, 11 },
                    { 25, 25 },
                    { 12, 12 },
                    { 14, 14 },
                    { 15, 15 },
                    { 16, 16 },
                    { 17, 17 },
                    { 18, 18 },
                    { 19, 19 },
                    { 20, 20 },
                    { 21, 21 },
                    { 22, 22 },
                    { 23, 23 },
                    { 13, 13 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "NeededResources",
                columns: new[] { "Id", "CraftableItemId", "NeededAmount", "RawResourceId", "RecipieId" },
                values: new object[,]
                {
                    { 5, 13, 1, null, 3 },
                    { 63, 20, 1, null, 31 },
                    { 64, 2, 1, null, 31 },
                    { 29, null, 50, 1, 14 },
                    { 25, null, 50, 4, 12 },
                    { 3, null, 50, 12, 2 },
                    { 41, null, 50, 13, 20 },
                    { 12, null, 50, 15, 6 },
                    { 33, null, 50, 20, 16 },
                    { 46, null, 100, 2, 23 },
                    { 61, null, 200, 2, 30 },
                    { 38, null, 50, 5, 19 },
                    { 20, null, 40, 6, 10 },
                    { 26, null, 100, 6, 13 },
                    { 62, 12, 1, null, 31 },
                    { 2, null, 600, 7, 1 },
                    { 1, null, 25, 10, 1 },
                    { 27, null, 200, 17, 13 },
                    { 47, null, 200, 18, 23 },
                    { 42, null, 250, 11, 21 },
                    { 13, null, 250, 16, 7 },
                    { 59, null, 250, 19, 29 },
                    { 14, null, 50, 3, 7 },
                    { 43, null, 50, 3, 21 },
                    { 60, null, 50, 3, 29 },
                    { 4, null, 50, 9, 2 },
                    { 24, null, 50, 9, 12 },
                    { 28, null, 50, 9, 14 },
                    { 40, null, 50, 9, 20 },
                    { 39, null, 400, 8, 19 },
                    { 58, 7, 1, null, 28 },
                    { 57, 26, 1, null, 28 },
                    { 56, 25, 1, null, 27 },
                    { 6, 23, 1, null, 3 },
                    { 7, 15, 1, null, 4 },
                    { 8, 29, 1, null, 4 },
                    { 9, 4, 1, null, 5 },
                    { 10, 18, 1, null, 5 },
                    { 15, 22, 1, null, 8 },
                    { 16, 21, 1, null, 8 },
                    { 17, 25, 1, null, 9 },
                    { 18, 24, 1, null, 9 },
                    { 19, 11, 1, null, 9 },
                    { 21, 6, 1, null, 11 },
                    { 22, 16, 1, null, 11 },
                    { 23, 14, 1, null, 11 },
                    { 30, 7, 1, null, 15 },
                    { 31, 21, 1, null, 15 },
                    { 34, 1, 1, null, 17 },
                    { 35, 30, 1, null, 17 },
                    { 36, 10, 5, null, 18 },
                    { 37, 19, 1, null, 18 },
                    { 44, 29, 1, null, 22 },
                    { 45, 7, 1, null, 22 },
                    { 48, 8, 1, null, 24 },
                    { 49, 17, 1, null, 24 },
                    { 50, 28, 1, null, 25 },
                    { 51, 3, 1, null, 25 },
                    { 52, 29, 1, null, 26 },
                    { 53, 21, 1, null, 26 },
                    { 54, 31, 1, null, 27 },
                    { 55, 5, 1, null, 27 },
                    { 11, null, 100, 14, 6 },
                    { 32, null, 100, 14, 16 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CraftableItems_ImageId",
                table: "CraftableItems",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededResources_CraftableItemId",
                table: "NeededResources",
                column: "CraftableItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededResources_RawResourceId",
                table: "NeededResources",
                column: "RawResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_NeededResources_RecipieId",
                table: "NeededResources",
                column: "RecipieId");

            migrationBuilder.CreateIndex(
                name: "IX_RawResources_ImageId",
                table: "RawResources",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_RawResources_RawResourceTypeId",
                table: "RawResources",
                column: "RawResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipies_ResultingItemId",
                table: "Recipies",
                column: "ResultingItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NeededResources");

            migrationBuilder.DropTable(
                name: "RawResources");

            migrationBuilder.DropTable(
                name: "Recipies");

            migrationBuilder.DropTable(
                name: "RawResourceTypes");

            migrationBuilder.DropTable(
                name: "CraftableItems");

            migrationBuilder.DropTable(
                name: "ImageContainers");
        }
    }
}
