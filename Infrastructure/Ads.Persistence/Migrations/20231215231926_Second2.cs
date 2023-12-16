using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Second2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SubCategoryAdverts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "SubCategoryAdverts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SubCategoryAdverts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CategoryAdverts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "CategoryAdverts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CategoryAdverts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 16, 2, 19, 26, 749, DateTimeKind.Local).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 16, 2, 19, 26, 749, DateTimeKind.Local).AddTicks(3930), new Guid("55b94f30-cbfe-4705-9974-74b068602a31") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SubCategoryAdverts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "SubCategoryAdverts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SubCategoryAdverts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CategoryAdverts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "CategoryAdverts");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CategoryAdverts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 16, 1, 14, 25, 12, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 16, 1, 14, 25, 12, DateTimeKind.Local).AddTicks(7661), new Guid("df4ac84e-aac7-4823-b4c3-8ef8964e9676") });
        }
    }
}
