using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackInternship.Data.Migrations
{
    public partial class AddResourcesToSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "UserId" },
                values: new object[] { 1, 0, "prvi post", new DateTime(2022, 1, 6, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "UserId" },
                values: new object[] { 2, 0, "drugi\n post", new DateTime(2022, 1, 6, 15, 32, 55, 724, DateTimeKind.Local).AddTicks(2398), 2 });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Category", "Content", "CreatedAt", "UserId" },
                values: new object[] { 3, 1, "t\nr\ne\nc\ni\npost", new DateTime(2022, 1, 6, 15, 32, 55, 724, DateTimeKind.Local).AddTicks(2521), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
