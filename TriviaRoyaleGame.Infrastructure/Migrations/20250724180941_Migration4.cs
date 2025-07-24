using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TriviaRoyaleGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreBoards_Users_PlayerId",
                table: "ScoreBoards");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "ScoreBoards",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreBoards_PlayerId",
                table: "ScoreBoards",
                newName: "IX_ScoreBoards_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreBoards_Users_UserId",
                table: "ScoreBoards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreBoards_Users_UserId",
                table: "ScoreBoards");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ScoreBoards",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreBoards_UserId",
                table: "ScoreBoards",
                newName: "IX_ScoreBoards_PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreBoards_Users_PlayerId",
                table: "ScoreBoards",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
