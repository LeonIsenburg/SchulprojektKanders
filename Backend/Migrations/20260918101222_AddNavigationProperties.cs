using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventLocationId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "ValidLocation",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Band",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Band", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventID = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    StartDay = table.Column<string>(type: "TEXT", nullable: false),
                    EntryTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    PriceType = table.Column<int>(type: "INTEGER", nullable: false),
                    EventType = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    MemberId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValidLocationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Event_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_ValidLocation_ValidLocationId",
                        column: x => x.ValidLocationId,
                        principalTable: "ValidLocation",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicianInstrument",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicianId = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstrumentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianInstrument", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_MusicianInstrument_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianInstrument_Musician_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musician",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandMusicGenre",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicGenreId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMusicGenre", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_BandMusicGenre_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandMusicGenre_MusicGenre_MusicGenreId",
                        column: x => x.MusicGenreId,
                        principalTable: "MusicGenre",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandMusician",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MusicianId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandMusician", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_BandMusician_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandMusician_Musician_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musician",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BandSong",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandSong", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_BandSong_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandSong_Song_SongId",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Concert",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConcertID = table.Column<int>(type: "INTEGER", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "Festival",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    FestivalID = table.Column<int>(type: "INTEGER", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndDay = table.Column<string>(type: "TEXT", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FestivalNameId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                    PartyID = table.Column<int>(type: "INTEGER", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndDay = table.Column<string>(type: "TEXT", nullable: false),
                    Organizer = table.Column<string>(type: "TEXT", nullable: false),
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartyNameId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "ConcertBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    ConcertId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                name: "FestivalBand",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    FestivalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Time = table.Column<TimeOnly>(type: "TEXT", nullable: true),
                    Day = table.Column<string>(type: "TEXT", nullable: true)
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
                    PartyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BandId = table.Column<Guid>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_CityId_DistrictId_RegionId_StateId_EventLocationId",
                table: "ValidLocation",
                columns: new[] { "CityId", "DistrictId", "RegionId", "StateId", "EventLocationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_DistrictId",
                table: "ValidLocation",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_EventLocationId",
                table: "ValidLocation",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_RegionId",
                table: "ValidLocation",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidLocation_StateId",
                table: "ValidLocation",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_State_Name",
                table: "State",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Song_Name",
                table: "Song",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Name",
                table: "Region",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partyname_Name",
                table: "Partyname",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musician_ArtistName",
                table: "Musician",
                column: "ArtistName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musician_FirstName_LastName",
                table: "Musician",
                columns: new[] { "FirstName", "LastName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicGenre_Description",
                table: "MusicGenre",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_Nickname",
                table: "Member",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_Username",
                table: "Member",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_Name",
                table: "Instrument",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FestivalName_Name",
                table: "FestivalName",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_Name",
                table: "EventLocation",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Name",
                table: "District",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Band_Name",
                table: "Band",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandMusicGenre_BandId_MusicGenreId",
                table: "BandMusicGenre",
                columns: new[] { "BandId", "MusicGenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandMusicGenre_MusicGenreId",
                table: "BandMusicGenre",
                column: "MusicGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMusician_BandId_MusicianId",
                table: "BandMusician",
                columns: new[] { "BandId", "MusicianId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandMusician_MusicianId",
                table: "BandMusician",
                column: "MusicianId");

            migrationBuilder.CreateIndex(
                name: "IX_BandSong_BandId_SongId",
                table: "BandSong",
                columns: new[] { "BandId", "SongId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BandSong_SongId",
                table: "BandSong",
                column: "SongId");

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
                name: "IX_Event_MemberId",
                table: "Event",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ValidLocationId",
                table: "Event",
                column: "ValidLocationId");

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
                name: "IX_MusicianInstrument_InstrumentId",
                table: "MusicianInstrument",
                column: "InstrumentId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianInstrument_MusicianId_InstrumentId",
                table: "MusicianInstrument",
                columns: new[] { "MusicianId", "InstrumentId" },
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "BandMusicGenre");

            migrationBuilder.DropTable(
                name: "BandMusician");

            migrationBuilder.DropTable(
                name: "BandSong");

            migrationBuilder.DropTable(
                name: "ConcertBand");

            migrationBuilder.DropTable(
                name: "FestivalBand");

            migrationBuilder.DropTable(
                name: "MusicianInstrument");

            migrationBuilder.DropTable(
                name: "PartyBand");

            migrationBuilder.DropTable(
                name: "Concert");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropTable(
                name: "Party");

            migrationBuilder.DropTable(
                name: "Band");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_CityId_DistrictId_RegionId_StateId_EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_DistrictId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_RegionId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_ValidLocation_StateId",
                table: "ValidLocation");

            migrationBuilder.DropIndex(
                name: "IX_State_Name",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_Song_Name",
                table: "Song");

            migrationBuilder.DropIndex(
                name: "IX_Region_Name",
                table: "Region");

            migrationBuilder.DropIndex(
                name: "IX_Partyname_Name",
                table: "Partyname");

            migrationBuilder.DropIndex(
                name: "IX_Musician_ArtistName",
                table: "Musician");

            migrationBuilder.DropIndex(
                name: "IX_Musician_FirstName_LastName",
                table: "Musician");

            migrationBuilder.DropIndex(
                name: "IX_MusicGenre_Description",
                table: "MusicGenre");

            migrationBuilder.DropIndex(
                name: "IX_Member_Nickname",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Member_Username",
                table: "Member");

            migrationBuilder.DropIndex(
                name: "IX_Instrument_Name",
                table: "Instrument");

            migrationBuilder.DropIndex(
                name: "IX_FestivalName_Name",
                table: "FestivalName");

            migrationBuilder.DropIndex(
                name: "IX_EventLocation_Name",
                table: "EventLocation");

            migrationBuilder.DropIndex(
                name: "IX_District_Name",
                table: "District");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "ValidLocation");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "ValidLocation");

            migrationBuilder.DropColumn(
                name: "EventLocationId",
                table: "ValidLocation");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "ValidLocation");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "ValidLocation");
        }
    }
}
