using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newurlimgPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "ImageURL",
                value: "assets/img/Menu/Pizza/Chorizzo.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "ImageURL",
                value: "https://photos.fife.usercontent.google.com/pw/AP1GczPjbg3nRj01AJP_kW1FMV_yeooafZOz8X0vJuFx1tGPOq8nHbshZFuE=w1200-h800-s-no-gm?authuser=0");
        }
    }
}
