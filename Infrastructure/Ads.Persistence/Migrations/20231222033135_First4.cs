using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class First4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
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
                value: new DateTime(2023, 12, 22, 6, 31, 35, 214, DateTimeKind.Local).AddTicks(8344));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 22, 6, 31, 35, 214, DateTimeKind.Local).AddTicks(8587), new Guid("c4173431-1e4d-4950-b60f-f916b29ef9a9") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pages");

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
    }
}
