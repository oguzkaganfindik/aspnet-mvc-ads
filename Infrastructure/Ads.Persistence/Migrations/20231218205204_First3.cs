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
            migrationBuilder.DropTable(
                name: "AdvertSliderImage");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 23, 52, 4, 265, DateTimeKind.Local).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 18, 23, 52, 4, 265, DateTimeKind.Local).AddTicks(2440), new Guid("4981036f-603f-4e72-b84c-7d7a94b79ab0") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertSliderImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvertId = table.Column<int>(type: "int", nullable: false),
                    AdvertSliderImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertSliderImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertSliderImage_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AdvertSliderImage_AdvertId",
                table: "AdvertSliderImage",
                column: "AdvertId");
        }
    }
}
