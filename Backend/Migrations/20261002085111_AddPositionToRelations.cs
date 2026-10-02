using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class AddPositionToRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "PartyBand",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "FestivalBand",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "ConcertBand",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "BandSong",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "BandMusician",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "BandMusicGenre",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "PartyBand");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "FestivalBand");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "ConcertBand");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "BandSong");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "BandMusician");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "BandMusicGenre");
        }
    }
}
