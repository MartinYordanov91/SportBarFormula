using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuantityToMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "MenuItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Shows how many grams or how many milliliters the given product is");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "Quantity",
                value: 480);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "MenuItems");
        }
    }
}
