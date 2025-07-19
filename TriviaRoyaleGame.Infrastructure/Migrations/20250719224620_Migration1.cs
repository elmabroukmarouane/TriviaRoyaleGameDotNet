using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TriviaRoyaleGame.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Choices = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectChoiceIndex = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberId = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    IsOnLine = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreateDate", "CreatedBy", "FirstName", "LastName", "UpdateDate", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Adrien", "RODRIGUES", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrat" },
                    { 2, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Ahmed", "NACIRI", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 3, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Ilias", "KADI", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 4, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "Marouane", "EL MABROUK", new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Choices", "CorrectChoiceIndex", "CreateDate", "CreatedBy", "Text", "UpdateDate", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "[\"London\",\"Paris\",\"Berlin\",\"Rome\"]", 1, null, null, "Capital of France ?", null, null },
                    { 2, "[\"3\",\"4\",\"5\",\"6\"]", 1, null, null, "2 + 2 = ?", null, null },
                    { 3, "[\"Python\",\"C#\",\"Java\",\"PHP\"]", 1, null, null, "Blazor is a ___ framework?", null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "CreatedBy", "Email", "IsOnLine", "MemberId", "Password", "Role", "UpdateDate", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "adrien@mail.com", false, 1, "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413", 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 2, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "ahmed@mail.com", false, 2, "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413", 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 3, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "iliad@mail.com", false, 3, "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413", 1, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" },
                    { 4, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator", "marouane@mail.com", false, 4, "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413", 3, new DateTime(2025, 6, 25, 14, 8, 11, 0, DateTimeKind.Unspecified), "Administrator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MemberId",
                table: "Users",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
