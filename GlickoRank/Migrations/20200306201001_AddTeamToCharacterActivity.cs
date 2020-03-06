using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class AddTeamToCharacterActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Team",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "CharacterActivity");
        }
    }
}
