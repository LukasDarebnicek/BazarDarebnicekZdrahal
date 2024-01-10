using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTB.Eshop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhoneEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Products",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "PhoneNumber",
                table: "Products",
                type: "double",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageSrc",
                value: "/img/carousel/silver-car.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageSrc",
                value: "/img/carousel/sekacka.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageSrc",
                value: "/img/carousel/Information-Technology-1-1.jpg");

            migrationBuilder.UpdateData(
                table: "Carousels",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageSrc",
                value: "/img/carousel/itec-index-banner.jpg");
        }
    }
}
