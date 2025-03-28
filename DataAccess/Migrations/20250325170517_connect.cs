using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class connect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "RentACars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentACars_CarId",
                table: "RentACars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_RentACars_LocationId",
                table: "RentACars",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CarPricings_CarId",
                table: "CarPricings",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPricings_Cars_CarId",
                table: "CarPricings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Cars_CarId",
                table: "RentACars",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Locations_LocationId",
                table: "RentACars",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPricings_Cars_CarId",
                table: "CarPricings");

            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Cars_CarId",
                table: "RentACars");

            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Locations_LocationId",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_RentACars_CarId",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_RentACars_LocationId",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_CarPricings_CarId",
                table: "CarPricings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "RentACars");
        }
    }
}
