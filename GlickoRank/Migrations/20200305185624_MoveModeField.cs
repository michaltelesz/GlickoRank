using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class MoveModeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "CharacterActivity");

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Activity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Activity");

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "CharacterActivity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
