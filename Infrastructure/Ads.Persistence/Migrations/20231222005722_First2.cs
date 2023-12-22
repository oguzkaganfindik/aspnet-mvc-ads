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
            migrationBuilder.RenameColumn(
                name: "IconPath",
                table: "Categories",
                newName: "CategoryIconPath");

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryIconPath",
                table: "SubCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryIconPath",
                table: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryIconPath",
                table: "Categories",
                newName: "IconPath");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 20, 12, 16, 607, DateTimeKind.Local).AddTicks(9955));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 21, 20, 12, 16, 608, DateTimeKind.Local).AddTicks(171), new Guid("be442ee7-c633-4842-a80f-c2fea348dda6") });
        }
    }
}
