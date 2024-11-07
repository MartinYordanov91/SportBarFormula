using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportBarFormula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMainEntitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the Category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category name (drinks, pizzas)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                },
                comment: "Group menu items by categories such as drinks, main courses, desserts.");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the feedback")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the user who provided the feedback"),
                    Rating = table.Column<int>(type: "int", nullable: false, comment: "Rating given by the user (e.g., from 1 to 5)"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "User's comment"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time when the feedback was created")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Stores customer feedback and ratings for service, menu, or events.");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the order")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the user who placed the order"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Order date"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Total amount of the order")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contains information about orders placed by customers.");

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the table")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Table number"),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "Table capacity"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Table location (e.g., indoor, outdoor)"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Table availability flag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                },
                comment: "Contains information about tables in the restaurant, such as table number and capacity.");

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the item")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Item name"),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true, comment: "Item description"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Item price"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the Category"),
                    ImageURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "Item Image URL"),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, comment: "Item availability flag"),
                    Ingredients = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "List of ingredients"),
                    PreparationTime = table.Column<int>(type: "int", nullable: false, comment: "Preparation time in minutes"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Soft delit flag")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Contains information about menu items - food, drinks and more.");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the payment")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the order associated with the payment"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the user who made the payment"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time of the payment"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Amount of the payment"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Payment method (e.g., cash, card)"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Payment status (e.g., successful, unsuccessful)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Tracks information about payments for orders.");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the reservation")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Identifier of the user who made the reservation"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Date and time of the reservation"),
                    TableId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the reserved table"),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false, comment: "Number of guests"),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false, comment: "Indicates whether the reservation is canceled")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Manages table reservations in the sports bar.");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier of the OrderItem")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "Order ID (foreign key to Orders table)"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false, comment: "Item ID (foreign key to MenuItems table)"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "Item quantity"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Item price at time of order vs. quantity")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "This table is the link between Orders and MenuItems. Each line in it represents one item in the order");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
