using Microsoft.EntityFrameworkCore.Migrations;

namespace GlickoRank.Migrations
{
    public partial class FixIndexInCharacterActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Activity_CharacterId",
                table: "CharacterActivity");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterActivity_ActivityId",
                table: "CharacterActivity",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityId",
                table: "CharacterActivity",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterActivity_Activity_ActivityId",
                table: "CharacterActivity");

            migrationBuilder.DropIndex(
                name: "IX_CharacterActivity_ActivityId",
                table: "CharacterActivity");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterActivity_Activity_CharacterId",
                table: "CharacterActivity",
                column: "CharacterId",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
