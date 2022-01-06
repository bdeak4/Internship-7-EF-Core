using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StackInternship.Data.Migrations
{
    public partial class AddNestedCommentsToSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 23, 4, 564, DateTimeKind.Local).AddTicks(7096));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "CreatedAt", "ParentId" },
                values: new object[] { "treci", new DateTime(2022, 1, 7, 0, 23, 4, 564, DateTimeKind.Local).AddTicks(7869), 2 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "ParentId", "ResourceId", "UserId" },
                values: new object[] { 4, "super post!", new DateTime(2022, 1, 7, 0, 23, 4, 564, DateTimeKind.Local).AddTicks(7923), null, 1, 3 });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 23, 4, 563, DateTimeKind.Local).AddTicks(9199));

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 23, 4, 563, DateTimeKind.Local).AddTicks(9432));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 20, 35, 265, DateTimeKind.Local).AddTicks(5602));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "CreatedAt", "ParentId" },
                values: new object[] { "super post!", new DateTime(2022, 1, 7, 0, 20, 35, 265, DateTimeKind.Local).AddTicks(6072), null });

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 20, 35, 265, DateTimeKind.Local).AddTicks(1841));

            migrationBuilder.UpdateData(
                table: "Resources",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2022, 1, 7, 0, 20, 35, 265, DateTimeKind.Local).AddTicks(1923));
        }
    }
}
