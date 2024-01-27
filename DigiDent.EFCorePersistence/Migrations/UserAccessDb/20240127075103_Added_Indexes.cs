using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiDent.EFCorePersistence.Migrations.UserAccessDb
{
    /// <inheritdoc />
    public partial class Added_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "User_Access",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0db83e32-7cca-42a5-b35b-7b9e96041b4f"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "User_Access",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                schema: "User_Access",
                table: "Users",
                columns: new[] { "Id", "Email", "Full Name", "Password", "PhoneNumber", "Role" },
                values: new object[] { new Guid("575dadc1-9fbf-492d-8b31-cdd61d567537"), "temp@admin.tmp", "Temporary Administrator", "CjEWdqOvMly9mNUYkGdjPunJ2yGRTwjMmp8tvA5vQkU=:By9pbtPc95ZkkDOAZ7obDmfOgAqlJXdqUTrPJQJv+Bc=", "+380000000000", "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "User_Access",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "User_Access",
                table: "Users");

            migrationBuilder.DeleteData(
                schema: "User_Access",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("575dadc1-9fbf-492d-8b31-cdd61d567537"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "User_Access",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                schema: "User_Access",
                table: "Users",
                columns: new[] { "Id", "Email", "Full Name", "Password", "PhoneNumber", "Role" },
                values: new object[] { new Guid("0db83e32-7cca-42a5-b35b-7b9e96041b4f"), "temp@admin.tmp", "Temporary Administrator", "jewtOvEUIx6ttNpKCWwtvnE+fT/h6zMGhELVFgRGBW4=:+KdLbLuio1Q1w5YuWdtxk4G6739OY6pE6yPaZ2zqBFQ=", "+380000000000", "Administrator" });
        }
    }
}
