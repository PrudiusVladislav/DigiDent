using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiDent.InventoryManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory_management");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "inventory_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "inventory_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                schema: "inventory_management",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ActionPerformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Employees_ActionPerformerId",
                        column: x => x.ActionPerformerId,
                        principalSchema: "inventory_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actions_Items_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalSchema: "inventory_management",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "inventory_management",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestedItemId = table.Column<int>(type: "int", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfRequest = table.Column<DateOnly>(type: "date", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_RequesterId",
                        column: x => x.RequesterId,
                        principalSchema: "inventory_management",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Items_RequestedItemId",
                        column: x => x.RequestedItemId,
                        principalSchema: "inventory_management",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ActionPerformerId",
                schema: "inventory_management",
                table: "Actions",
                column: "ActionPerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_InventoryItemId",
                schema: "inventory_management",
                table: "Actions",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Name",
                schema: "inventory_management",
                table: "Items",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestedItemId",
                schema: "inventory_management",
                table: "Requests",
                column: "RequestedItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                schema: "inventory_management",
                table: "Requests",
                column: "RequesterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions",
                schema: "inventory_management");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "inventory_management");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "inventory_management");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "inventory_management");
        }
    }
}
