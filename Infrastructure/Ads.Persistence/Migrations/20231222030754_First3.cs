using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class First3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Pages",
                newName: "Title2");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Pages",
                newName: "Content2");

            migrationBuilder.AddColumn<string>(
                name: "Content1",
                table: "Pages",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title1",
                table: "Pages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 22, 6, 7, 54, 324, DateTimeKind.Local).AddTicks(7278));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 22, 6, 7, 54, 324, DateTimeKind.Local).AddTicks(7420), new Guid("ffe5f691-d185-4175-bb05-db176251f582") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content1",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Title1",
                table: "Pages");

            migrationBuilder.RenameColumn(
                name: "Title2",
                table: "Pages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Content2",
                table: "Pages",
                newName: "Content");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 22, 3, 57, 22, 290, DateTimeKind.Local).AddTicks(4173));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 22, 3, 57, 22, 290, DateTimeKind.Local).AddTicks(4378), new Guid("7828d444-e666-4a52-b03a-7a67ccc6061e") });
        }
    }
}
