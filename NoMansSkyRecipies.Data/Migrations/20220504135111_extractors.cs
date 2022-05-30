using Microsoft.EntityFrameworkCore.Migrations;

namespace NoMansSkyRecipies.Data.Migrations
{
    public partial class extractors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extractors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtractionRate = table.Column<int>(nullable: false),
                    MiningOutpostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extractors_MiningOutposts_MiningOutpostId",
                        column: x => x.MiningOutpostId,
                        principalTable: "MiningOutposts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Extractors_MiningOutpostId",
                table: "Extractors",
                column: "MiningOutpostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extractors");
        }
    }
}
