using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarDescriptions_Cars_CarId",
                table: "CarDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CarFeatures_CarId",
                table: "CarFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CarFeatures_FeatureId",
                table: "CarFeatures");

            migrationBuilder.DropIndex(
                name: "IX_CarDescriptions_CarId",
                table: "CarDescriptions");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_CarFeatures_CarId",
                table: "CarFeatures",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarFeatures_FeatureId",
                table: "CarFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CarDescriptions_CarId",
                table: "CarDescriptions",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarDescriptions_Cars_CarId",
                table: "CarDescriptions",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Cars_CarId",
                table: "CarFeatures",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarFeatures_Features_FeatureId",
                table: "CarFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
