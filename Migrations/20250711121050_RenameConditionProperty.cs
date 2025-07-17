using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booklend.Migrations
{
    /// <inheritdoc />
    public partial class RenameConditionProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRating_Books_BookId",
                table: "BookRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRating",
                table: "BookRating");

            migrationBuilder.RenameTable(
                name: "BookRating",
                newName: "BookRatings");

            migrationBuilder.RenameColumn(
                name: "BookCondition",
                table: "BookItems",
                newName: "Condition");

            migrationBuilder.RenameIndex(
                name: "IX_BookRating_BookId",
                table: "BookRatings",
                newName: "IX_BookRatings_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRatings",
                table: "BookRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRatings_Books_BookId",
                table: "BookRatings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRatings_Books_BookId",
                table: "BookRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRatings",
                table: "BookRatings");

            migrationBuilder.RenameTable(
                name: "BookRatings",
                newName: "BookRating");

            migrationBuilder.RenameColumn(
                name: "Condition",
                table: "BookItems",
                newName: "BookCondition");

            migrationBuilder.RenameIndex(
                name: "IX_BookRatings_BookId",
                table: "BookRating",
                newName: "IX_BookRating_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRating",
                table: "BookRating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRating_Books_BookId",
                table: "BookRating",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
