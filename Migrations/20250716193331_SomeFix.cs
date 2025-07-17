using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booklend.Migrations
{
    /// <inheritdoc />
    public partial class SomeFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "BookRatings");

            migrationBuilder.AddColumn<decimal>(
                name: "Note",
                table: "BookRatings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "BookRatings");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "BookRatings",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
