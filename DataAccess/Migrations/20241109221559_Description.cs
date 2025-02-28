using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPricings_Cars_CarId",
                table: "CarPricings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPricings_Pricings_PricingId",
                table: "CarPricings");

            migrationBuilder.DropIndex(
                name: "IX_CarPricings_CarId",
                table: "CarPricings");

            migrationBuilder.DropIndex(
                name: "IX_CarPricings_PricingId",
                table: "CarPricings");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_CarPricings_CarId",
                table: "CarPricings",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPricings_PricingId",
                table: "CarPricings",
                column: "PricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPricings_Cars_CarId",
                table: "CarPricings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPricings_Pricings_PricingId",
                table: "CarPricings",
                column: "PricingId",
                principalTable: "Pricings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
