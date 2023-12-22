using Microsoft.EntityFrameworkCore.Migrations;


#nullable disable

namespace Ads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 22, 14, 34, 4, 235, DateTimeKind.Local).AddTicks(4533));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 22, 14, 34, 4, 235, DateTimeKind.Local).AddTicks(4746), new Guid("7d6b8ecb-15ba-4126-b473-13b46dc3ab85") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
