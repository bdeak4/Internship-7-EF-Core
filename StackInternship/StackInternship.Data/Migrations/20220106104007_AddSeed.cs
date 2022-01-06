using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackInternship.Data.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeactivatedUntil", "HashedPassword", "IsOrganizer", "Password", "Username" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new byte[] { 98, 49, 48, 57, 102, 51, 98, 98, 98, 99, 50, 52, 52, 101, 98, 56, 50, 52, 52, 49, 57, 49, 55, 101, 100, 48, 54, 100, 54, 49, 56, 98, 57, 48, 48, 56, 100, 100, 48, 57, 98, 51, 98, 101, 102, 100, 49, 98, 53, 101, 48, 55, 51, 57, 52, 99, 55, 48, 54, 97, 56, 98, 98, 57, 56, 48, 98, 49, 100, 55, 55, 56, 53, 101, 53, 57, 55, 54, 101, 99, 48, 52, 57, 98, 52, 54, 100, 102, 53, 102, 49, 51, 50, 54, 97, 102, 53, 97, 50, 101, 97, 54, 100, 49, 48, 51, 102, 100, 48, 55, 99, 57, 53, 51, 56, 53, 102, 102, 97, 98, 48, 99, 97, 99, 98, 99, 56, 54 }, false, null, "ivan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeactivatedUntil", "HashedPassword", "IsOrganizer", "Password", "Username" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new byte[] { 98, 49, 48, 57, 102, 51, 98, 98, 98, 99, 50, 52, 52, 101, 98, 56, 50, 52, 52, 49, 57, 49, 55, 101, 100, 48, 54, 100, 54, 49, 56, 98, 57, 48, 48, 56, 100, 100, 48, 57, 98, 51, 98, 101, 102, 100, 49, 98, 53, 101, 48, 55, 51, 57, 52, 99, 55, 48, 54, 97, 56, 98, 98, 57, 56, 48, 98, 49, 100, 55, 55, 56, 53, 101, 53, 57, 55, 54, 101, 99, 48, 52, 57, 98, 52, 54, 100, 102, 53, 102, 49, 51, 50, 54, 97, 102, 53, 97, 50, 101, 97, 54, 100, 49, 48, 51, 102, 100, 48, 55, 99, 57, 53, 51, 56, 53, 102, 102, 97, 98, 48, 99, 97, 99, 98, 99, 56, 54 }, true, null, "marko" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeactivatedUntil", "HashedPassword", "IsOrganizer", "Password", "Username" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new byte[] { 98, 49, 48, 57, 102, 51, 98, 98, 98, 99, 50, 52, 52, 101, 98, 56, 50, 52, 52, 49, 57, 49, 55, 101, 100, 48, 54, 100, 54, 49, 56, 98, 57, 48, 48, 56, 100, 100, 48, 57, 98, 51, 98, 101, 102, 100, 49, 98, 53, 101, 48, 55, 51, 57, 52, 99, 55, 48, 54, 97, 56, 98, 98, 57, 56, 48, 98, 49, 100, 55, 55, 56, 53, 101, 53, 57, 55, 54, 101, 99, 48, 52, 57, 98, 52, 54, 100, 102, 53, 102, 49, 51, 50, 54, 97, 102, 53, 97, 50, 101, 97, 54, 100, 49, 48, 51, 102, 100, 48, 55, 99, 57, 53, 51, 56, 53, 102, 102, 97, 98, 48, 99, 97, 99, 98, 99, 56, 54 }, true, null, "ante" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
