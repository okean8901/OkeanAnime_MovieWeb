using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Okean_AnimeMovie.Migrations
{
    /// <inheritdoc />
    public partial class AddViewHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: false),
                    WatchedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WatchDuration = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewHistories_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ViewHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ViewHistories_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViewHistories_AnimeId",
                table: "ViewHistories",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewHistories_EpisodeId",
                table: "ViewHistories",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewHistories_UserId_AnimeId_EpisodeId_WatchedAt",
                table: "ViewHistories",
                columns: new[] { "UserId", "AnimeId", "EpisodeId", "WatchedAt" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewHistories");
        }
    }
}
