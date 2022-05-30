using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class Miningrelatedentitiesandlogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HotspotClasses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class = table.Column<string>(nullable: false),
                    MaxOutput = table.Column<int>(nullable: false),
                    MaxConcentration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotspotClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectroMagneticPlants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotspotClassId = table.Column<int>(nullable: false),
                    HotspotClassId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectroMagneticPlants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectroMagneticPlants_HotspotClasses_HotspotClassId",
                        column: x => x.HotspotClassId,
                        principalTable: "HotspotClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElectroMagneticPlants_HotspotClasses_HotspotClassId1",
                        column: x => x.HotspotClassId1,
                        principalTable: "HotspotClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectromagneticGenerators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Output = table.Column<int>(nullable: false),
                    PlantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectromagneticGenerators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectromagneticGenerators_ElectroMagneticPlants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "ElectroMagneticPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergySupplies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolarPanels = table.Column<int>(nullable: false),
                    Batteries = table.Column<int>(nullable: false),
                    PlantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergySupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergySupplies_ElectroMagneticPlants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "ElectroMagneticPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MiningOutposts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceTypeId = table.Column<int>(nullable: false),
                    HotspotClassId = table.Column<int>(nullable: false),
                    ExtractorCount = table.Column<int>(nullable: false),
                    EnergySupplyId = table.Column<int>(nullable: false),
                    SupplyDepots = table.Column<int>(nullable: false),
                    HaveTeleport = table.Column<bool>(nullable: false),
                    HotspotClassId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MiningOutposts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MiningOutposts_EnergySupplies_EnergySupplyId",
                        column: x => x.EnergySupplyId,
                        principalTable: "EnergySupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MiningOutposts_HotspotClasses_HotspotClassId",
                        column: x => x.HotspotClassId,
                        principalTable: "HotspotClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MiningOutposts_HotspotClasses_HotspotClassId1",
                        column: x => x.HotspotClassId1,
                        principalTable: "HotspotClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MiningOutposts_RawResourceTypes_ResourceTypeId",
                        column: x => x.ResourceTypeId,
                        principalTable: "RawResourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HotspotClasses",
                columns: new[] { "Id", "Class", "MaxConcentration", "MaxOutput" },
                values: new object[,]
                {
                    { 2, "A", 80, 0 },
                    { 3, "A", 60, 0 },
                    { 4, "A", 40, 0 },
                    { 1, "S", 100, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectromagneticGenerators_PlantId",
                table: "ElectromagneticGenerators",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectroMagneticPlants_HotspotClassId",
                table: "ElectroMagneticPlants",
                column: "HotspotClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectroMagneticPlants_HotspotClassId1",
                table: "ElectroMagneticPlants",
                column: "HotspotClassId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnergySupplies_PlantId",
                table: "EnergySupplies",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_MiningOutposts_EnergySupplyId",
                table: "MiningOutposts",
                column: "EnergySupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_MiningOutposts_HotspotClassId",
                table: "MiningOutposts",
                column: "HotspotClassId");

            migrationBuilder.CreateIndex(
                name: "IX_MiningOutposts_HotspotClassId1",
                table: "MiningOutposts",
                column: "HotspotClassId1");

            migrationBuilder.CreateIndex(
                name: "IX_MiningOutposts_ResourceTypeId",
                table: "MiningOutposts",
                column: "ResourceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectromagneticGenerators");

            migrationBuilder.DropTable(
                name: "MiningOutposts");

            migrationBuilder.DropTable(
                name: "EnergySupplies");

            migrationBuilder.DropTable(
                name: "ElectroMagneticPlants");

            migrationBuilder.DropTable(
                name: "HotspotClasses");
        }
    }
}
