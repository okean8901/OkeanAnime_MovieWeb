using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Okean_AnimeMovie.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRatingFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpfulCount",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "IsSpoiler",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "ReviewText",
                table: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelpfulCount",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Ratings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpoiler",
                table: "Ratings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReviewText",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
