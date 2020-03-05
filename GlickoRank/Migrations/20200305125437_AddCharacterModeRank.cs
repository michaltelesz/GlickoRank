using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class AddCharacterModeRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityId",
                table: "CharacterActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Character_CharacterId",
                table: "CharacterActivity");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "CharacterActivity",
                newName: "ActivityID");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "CharacterActivity",
                newName: "CharacterID");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterActivity_ActivityId",
                table: "CharacterActivity",
                newName: "IX_CharacterActivity_ActivityID");

            migrationBuilder.CreateTable(
                name: "CharacterModeRank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterID = table.Column<int>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false, defaultValue: 1500f),
                    RD = table.Column<float>(nullable: false, defaultValue: 350f),
                    Volatility = table.Column<float>(nullable: false, defaultValue: 0.06f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterModeRank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CharacterModeRank_Character_CharacterID",
                        column: x => x.CharacterID,
                        principalTable: "Character",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "MajkPascal_1_2305843009296116294");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterModeRank_CharacterID",
                table: "CharacterModeRank",
                column: "CharacterID");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityID",
                table: "CharacterActivity",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Character_CharacterID",
                table: "CharacterActivity",
                column: "CharacterID",
                principalTable: "Character",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityID",
                table: "CharacterActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Character_CharacterID",
                table: "CharacterActivity");

            migrationBuilder.DropTable(
                name: "CharacterModeRank");

            migrationBuilder.RenameColumn(
                name: "ActivityID",
                table: "CharacterActivity",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "CharacterID",
                table: "CharacterActivity",
                newName: "CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterActivity_ActivityID",
                table: "CharacterActivity",
                newName: "IX_CharacterActivity_ActivityId");

            migrationBuilder.UpdateData(
                table: "Character",
                keyColumn: "ID",
                keyValue: 1,
                column: "Name",
                value: "MajkPascal_2305843009296116294_1");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityId",
                table: "CharacterActivity",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Character_CharacterId",
                table: "CharacterActivity",
                column: "CharacterId",
                principalTable: "Character",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
