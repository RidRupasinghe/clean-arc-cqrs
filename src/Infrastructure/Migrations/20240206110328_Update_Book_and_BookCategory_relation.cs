using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clean_arc_api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_Book_and_BookCategory_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_ListId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ListId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_CategoryId",
                table: "Books",
                column: "CategoryId",
                principalTable: "BookCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_CategoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CategoryId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_ListId",
                table: "Books",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_ListId",
                table: "Books",
                column: "ListId",
                principalTable: "BookCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
