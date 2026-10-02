using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class NewEventFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Event\";");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_ValidLocation_ValidLocationId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_Cities_CityId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_District_DistrictId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_EventLocation_EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_Region_RegionId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_State_StateId",
                table: "ValidLocation");

            migrationBuilder.DropTable(
                name: "ConcertBand");

            migrationBuilder.DropTable(
                name: "FestivalBand");

            migrationBuilder.DropTable(
                name: "PartyBand");

            migrationBuilder.DropTable(
                name: "TourBand");

            migrationBuilder.DropTable(
                name: "TourStop");

            migrationBuilder.DropTable(
                name: "Concert");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "FestivalName");

            migrationBuilder.DropTable(
                name: "Partyname");

            migrationBuilder.DropTable(
                name: "TourName");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_CityId_DistrictId_RegionId_StateId_EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_Musician_FirstName_LastName",
                table: "Musician");

            migrationBuilder.DropIndex(
                name: "IX_Event_ValidLocationId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "ValidLocationId",
                table: "Event",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Event",
                newName: "ExtraInfo");

            migrationBuilder.RenameColumn(
                name: "PriceType",
                table: "Event",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventLocationId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "DistrictId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ValidLocation",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Musician",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Musician",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "AgeRestriction",
                table: "Event",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizerId",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TikTok",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Event",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventArtist",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Performance = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventArtist", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EventArtist_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventArtist_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventDate",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DoorsOpen = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ValidLocationId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    Presale = table.Column<bool>(type: "INTEGER", nullable: true),
                    TicketUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDate", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_EventDate_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventDate_ValidLocation_ValidLocationId",
                        column: x => x.ValidLocationId,
                        principalTable: "ValidLocation",
                        principalColumn: "Guid");
                });

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizerID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_CityId",
                table: "ValidLocation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_OrganizerId",
                table: "Event",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_EventArtist_BandId",
                table: "EventArtist",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_EventArtist_EventId_BandId",
                table: "EventArtist",
                columns: new[] { "EventId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventDate_EventId_Position",
                table: "EventDate",
                columns: new[] { "EventId", "Position" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventDate_ValidLocationId",
                table: "EventDate",
                column: "ValidLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizer_Name",
                table: "Organizer",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Organizer_OrganizerId",
                table: "Event",
                column: "OrganizerId",
                principalTable: "Organizer",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_Cities_CityId",
                table: "ValidLocation",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_District_DistrictId",
                table: "ValidLocation",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_EventLocation_EventLocationId",
                table: "ValidLocation",
                column: "EventLocationId",
                principalTable: "EventLocation",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_Region_RegionId",
                table: "ValidLocation",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_State_StateId",
                table: "ValidLocation",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Organizer_OrganizerId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_Cities_CityId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_District_DistrictId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_EventLocation_EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_Region_RegionId",
                table: "ValidLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidLocation_State_StateId",
                table: "ValidLocation");

            migrationBuilder.DropTable(
                name: "EventArtist");

            migrationBuilder.DropTable(
                name: "EventDate");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_CityId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_Event_OrganizerId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ValidLocation");

            migrationBuilder.DropColumn(
                name: "AgeRestriction",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "TikTok",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Event",
                newName: "PriceType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Event",
                newName: "ValidLocationId");

            migrationBuilder.RenameColumn(
                name: "ExtraInfo",
                table: "Event",
                newName: "StartTime");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StateId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RegionId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventLocationId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DistrictId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CityId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Musician",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Musician",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntryTime",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "EventType",
                table: "Event",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Event",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "StartDay",
                table: "Event",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConcertID = table.Column<int>(type: "INTEGER", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concert", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Concert_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Concert_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FestivalName",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Festival_NameID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalName", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Partyname",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Party_NameID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partyname", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "TourName",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Tour_NameID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourName", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ConcertBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConcertId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertBand", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ConcertBand_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConcertBand_Concert_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concert",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FestivalNameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDay = table.Column<string>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    FestivalID = table.Column<int>(type: "INTEGER", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Festival_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Festival_FestivalName_FestivalNameId",
                        column: x => x.FestivalNameId,
                        principalTable: "FestivalName",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Party",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartyNameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndDay = table.Column<string>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    PartyID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Party", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Party_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Party_Partyname_PartyNameId",
                        column: x => x.PartyNameId,
                        principalTable: "Partyname",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourNameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    TourID = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "FestivalBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FestivalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Day = table.Column<string>(type: "TEXT", nullable: true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<TimeOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FestivalBand", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_FestivalBand_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FestivalBand_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartyBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyBand", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_PartyBand_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyBand_Party_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Party",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TourId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    ValidLocationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Day = table.Column<string>(type: "TEXT", nullable: false),
                    EntryTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    PriceType = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false)
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
                name: "IX_ValidLocation_CityId_DistrictId_RegionId_StateId_EventLocationId",
                table: "ValidLocation",
                columns: new[] { "CityId", "DistrictId", "RegionId", "StateId", "EventLocationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musician_FirstName_LastName",
                table: "Musician",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_ValidLocationId",
                table: "Event",
                column: "ValidLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_BandId",
                table: "Concert",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_EventId",
                table: "Concert",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConcertBand_BandId",
                table: "ConcertBand",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertBand_ConcertId_BandId",
                table: "ConcertBand",
                columns: new[] { "ConcertId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Festival_EventId",
                table: "Festival",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Festival_FestivalNameId_EndDate",
                table: "Festival",
                columns: new[] { "FestivalNameId", "EndDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FestivalBand_BandId",
                table: "FestivalBand",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_FestivalBand_FestivalId_BandId",
                table: "FestivalBand",
                columns: new[] { "FestivalId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FestivalName_Name",
                table: "FestivalName",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_EventId",
                table: "Party",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Party_PartyNameId_EndDate",
                table: "Party",
                columns: new[] { "PartyNameId", "EndDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartyBand_BandId",
                table: "PartyBand",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyBand_PartyId_BandId",
                table: "PartyBand",
                columns: new[] { "PartyId", "BandId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partyname_Name",
                table: "Partyname",
                column: "Name",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Event_ValidLocation_ValidLocationId",
                table: "Event",
                column: "ValidLocationId",
                principalTable: "ValidLocation",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_Cities_CityId",
                table: "ValidLocation",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_District_DistrictId",
                table: "ValidLocation",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_EventLocation_EventLocationId",
                table: "ValidLocation",
                column: "EventLocationId",
                principalTable: "EventLocation",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_Region_RegionId",
                table: "ValidLocation",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidLocation_State_StateId",
                table: "ValidLocation",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
