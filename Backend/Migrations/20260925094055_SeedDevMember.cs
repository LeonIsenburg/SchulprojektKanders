using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedDevMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "Guid", "Birthday", "EMail", "FirstName", "Gender", "HouseNumber", "LastName", "Location", "MemberID", "Nickname", "Password", "PhoneNumber", "PostalCode", "Role", "Street", "Username" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new DateOnly(2000, 1, 1), "dev@example.com", "Test", "divers", "1", "Mitglied", "Teststadt", 1, "Dev", "dev", "0000000000", "00000", 0, "Teststraße", "dev" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "Guid",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
