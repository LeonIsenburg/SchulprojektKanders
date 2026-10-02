using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddTour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourName",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tour_NameID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourName", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourID = table.Column<int>(type: "INTEGER", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourNameId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Tour_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tour_TourName_TourNameId",
                        column: x => x.TourNameId,
                        principalTable: "TourName",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBand", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_TourBand_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourBand_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourStop",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EntryTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    PriceType = table.Column<int>(type: "INTEGER", nullable: false),
                    ValidLocationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourStop", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_TourStop_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourStop_ValidLocation_ValidLocationId",
                        column: x => x.ValidLocationId,
                        principalTable: "ValidLocation",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_EventId",
                table: "Tour",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tour_TourNameId",
                table: "Tour",
                column: "TourNameId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBand_BandId",
                table: "TourBand",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_TourBand_TourId_BandId",
                table: "TourBand",
                columns: new[] { "TourId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourName_Name",
                table: "TourName",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourStop_TourId_Position",
                table: "TourStop",
                columns: new[] { "TourId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourStop_ValidLocationId",
                table: "TourStop",
                column: "ValidLocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourBand");

            migrationBuilder.DropTable(
                name: "TourStop");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "TourName");
        }
    }
}
