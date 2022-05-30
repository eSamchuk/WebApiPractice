using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CityInfrastructure.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubwayLines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubwayLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubwayStations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    LineId = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    ConstructionDate = table.Column<DateTime>(nullable: false),
                    IntersectingStationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubwayStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubwayStations_SubwayStations_IntersectingStationId",
                        column: x => x.IntersectingStationId,
                        principalTable: "SubwayStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubwayStations_SubwayLines_LineId",
                        column: x => x.LineId,
                        principalTable: "SubwayLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubwayStationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(nullable: false),
                    SubwayStationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubwayStationStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubwayStationStatuses_SubwayStations_SubwayStationId",
                        column: x => x.SubwayStationId,
                        principalTable: "SubwayStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(nullable: false),
                    ArriveTime = table.Column<DateTime>(nullable: false),
                    StationId = table.Column<int>(nullable: false),
                    RouteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleRecords_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Longtitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    RouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    ScheduleId = table.Column<int>(nullable: false),
                    RouteTypeId = table.Column<int>(nullable: false),
                    StationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_RouteTypes_RouteTypeId",
                        column: x => x.RouteTypeId,
                        principalTable: "RouteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Routes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Routes_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_RouteTypeId",
                table: "Routes",
                column: "RouteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ScheduleId",
                table: "Routes",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Routes_StationId",
                table: "Routes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleRecords_RouteId",
                table: "ScheduleRecords",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleRecords_ScheduleId",
                table: "ScheduleRecords",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleRecords_StationId",
                table: "ScheduleRecords",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_RouteId",
                table: "Station",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_SubwayStations_IntersectingStationId",
                table: "SubwayStations",
                column: "IntersectingStationId",
                unique: true,
                filter: "[IntersectingStationId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SubwayStations_LineId",
                table: "SubwayStations",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_SubwayStationStatuses_SubwayStationId",
                table: "SubwayStationStatuses",
                column: "SubwayStationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRecords_Station_StationId",
                table: "ScheduleRecords",
                column: "StationId",
                principalTable: "Station",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleRecords_Routes_RouteId",
                table: "ScheduleRecords",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Station_Routes_RouteId",
                table: "Station",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_RouteTypes_RouteTypeId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Schedules_ScheduleId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Station_StationId",
                table: "Routes");

            migrationBuilder.DropTable(
                name: "ScheduleRecords");

            migrationBuilder.DropTable(
                name: "SubwayStationStatuses");

            migrationBuilder.DropTable(
                name: "SubwayStations");

            migrationBuilder.DropTable(
                name: "SubwayLines");

            migrationBuilder.DropTable(
                name: "RouteTypes");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
