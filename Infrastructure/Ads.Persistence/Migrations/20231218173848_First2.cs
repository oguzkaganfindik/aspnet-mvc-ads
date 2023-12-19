using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class First2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 20, 38, 48, 271, DateTimeKind.Local).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 18, 20, 38, 48, 271, DateTimeKind.Local).AddTicks(7856), new Guid("70f64220-402a-4782-9457-09389bb0be64") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 18, 3, 40, 293, DateTimeKind.Local).AddTicks(3830));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 18, 18, 3, 40, 293, DateTimeKind.Local).AddTicks(3978), new Guid("0babfa4b-096d-47bd-9360-8ea0c1b29af3") });
        }
    }
}
