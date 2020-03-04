using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class AddActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterId",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipId",
                table: "Character",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MembershipType",
                table: "Character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<DateTime>(nullable: false),
                    InstanceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CharacterActivity",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterActivity", x => new { x.CharacterId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_CharacterActivity_Activity_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Activity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterActivity_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterActivity");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "MembershipType",
                table: "Character");
        }
    }
}
