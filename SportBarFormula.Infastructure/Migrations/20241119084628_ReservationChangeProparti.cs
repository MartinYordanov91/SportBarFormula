using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReservationChangeProparti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservations",
                type: "int",
                nullable: true,
                comment: "Identifier of the reserved table",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Identifier of the reserved table");

            migrationBuilder.AddColumn<bool>(
                name: "IsIndor",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "shows where the table is (indoor, outdoor)");

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "IsAvailable", "Location", "TableNumber" },
                values: new object[,]
                {
                    { 1, 6, true, "indoor", "100" },
                    { 2, 6, true, "indoor", "101" },
                    { 3, 6, true, "indoor", "102" },
                    { 4, 6, true, "indoor", "103" },
                    { 5, 6, true, "indoor", "104" },
                    { 6, 6, true, "indoor", "105" },
                    { 7, 6, true, "indoor", "106" },
                    { 8, 4, true, "indoor", "107" },
                    { 9, 4, true, "indoor", "108" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "IsIndor",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "TableId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Identifier of the reserved table",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Identifier of the reserved table");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
