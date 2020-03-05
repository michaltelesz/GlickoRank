using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class AddCharacterActivityModeRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Standing",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TeamScore",
                table: "CharacterActivity",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "CharacterActivityModeRank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterActivityCharacterID = table.Column<int>(nullable: false),
                    CharacterActivityActivityID = table.Column<int>(nullable: false),
                    CharacterActivityID = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false, defaultValue: 1500f),
                    RD = table.Column<float>(nullable: false, defaultValue: 350f),
                    Volatility = table.Column<float>(nullable: false, defaultValue: 0.06f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterActivityModeRank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CharacterActivityModeRank_CharacterActivity_CharacterActivityCharacterID_CharacterActivityActivityID",
                        columns: x => new { x.CharacterActivityCharacterID, x.CharacterActivityActivityID },
                        principalTable: "CharacterActivity",
                        principalColumns: new[] { "CharacterID", "ActivityID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterActivityModeRank_CharacterActivityCharacterID_CharacterActivityActivityID",
                table: "CharacterActivityModeRank",
                columns: new[] { "CharacterActivityCharacterID", "CharacterActivityActivityID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterActivityModeRank");

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "CharacterActivity");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "CharacterActivity");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "CharacterActivity");

            migrationBuilder.DropColumn(
                name: "Standing",
                table: "CharacterActivity");

            migrationBuilder.DropColumn(
                name: "TeamScore",
                table: "CharacterActivity");
        }
    }
}
