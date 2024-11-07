using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addShiftModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the shift")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the user assigned to the shift"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Start time of the shift"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "End time of the shift"),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Role of the employee during the shift")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Manages employee shifts to organize working hours.");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_UserId",
                table: "Shifts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
