using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZaptimeChatApp.Server.Data.Migrations
{
    public partial class AddedPasswordResetToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordRestToken",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExpires",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordRestToken",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ResetTokenExpires",
                table: "User");
        }
    }
}
