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
                columns: new[] { "Id", "Email", "Full Name", "Password", "Role" },
                values: new object[] { new Guid("77bf255c-c6f1-4e84-b5c9-9622ebf8b097"), "temp@admin.tmp", "Temporary Administrator", "RoWCsmdl9X5+v9wBXxDNmD3z1LATpfYxFLFdUXQ+/PI=:/YHVABtHq6neX2S48I0J2P3zY/kujBIB5dZN0CcbrdI=", "Administrator" });

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
