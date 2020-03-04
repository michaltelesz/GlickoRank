using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class SeedCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "ID", "CharacterId", "MembershipId", "MembershipType", "Name" },
                values: new object[] { 1, "2305843009296116294", "4611686018470345232", 1, "MajkPascal_2305843009296116294_1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Character",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
