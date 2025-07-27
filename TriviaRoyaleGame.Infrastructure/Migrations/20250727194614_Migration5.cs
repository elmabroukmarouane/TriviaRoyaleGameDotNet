using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TriviaRoyaleGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "CreatedBy", "Name", "UpdateDate", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Geography", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrat" },
                    { 2, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Science", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 3, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Math", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 4, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Computer Science", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" }
                });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Text" },
                values: new object[] { 4, "Blazor is a ___ framework ?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CategoryId", "Choices", "CorrectChoiceIndex", "CreateDate", "CreatedBy", "Text", "UpdateDate", "UpdatedBy" },
                values: new object[] { 4, 2, "[\"5.12\",\"9.8\",\"2\",\"10\"]", 1, null, null, "What is the gravity G of earth ?", null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Questions");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Text",
                value: "Blazor is a ___ framework?");
        }
    }
}
