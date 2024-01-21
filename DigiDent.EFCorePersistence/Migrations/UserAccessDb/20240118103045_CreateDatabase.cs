using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiDent.EFCorePersistence.Migrations.UserAccessDb
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "User_Access");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "User_Access",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(name: "Full Name", type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "User_Access",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "User_Access",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "User_Access",
                table: "Users",
                columns: new[] { "Id", "Email", "Full Name", "Password", "PhoneNumber", "Role" },
                values: new object[] { new Guid("0db83e32-7cca-42a5-b35b-7b9e96041b4f"), "temp@admin.tmp", "Temporary Administrator", "jewtOvEUIx6ttNpKCWwtvnE+fT/h6zMGhELVFgRGBW4=:+KdLbLuio1Q1w5YuWdtxk4G6739OY6pE6yPaZ2zqBFQ=", "+380000000000", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "User_Access",
                table: "RefreshTokens",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "User_Access");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "User_Access");
        }
    }
}
