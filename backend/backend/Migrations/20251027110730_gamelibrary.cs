using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class gamelibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGameLibraries",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    PurchasedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalPlaytime = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGameLibraries", x => new { x.userId, x.GameId });
                    table.ForeignKey(
                        name: "FK_UserGameLibraries_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGameLibraries_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGameLibraries_GameId",
                table: "UserGameLibraries",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGameLibraries");
        }
    }
}
