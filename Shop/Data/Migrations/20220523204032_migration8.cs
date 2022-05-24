using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "ProductCategoryId",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 23, 22, 40, 32, 8, DateTimeKind.Local).AddTicks(1765), new DateTime(2022, 5, 23, 22, 40, 32, 8, DateTimeKind.Local).AddTicks(1730) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ProductCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 21, 23, 20, 52, 430, DateTimeKind.Local).AddTicks(4753), new DateTime(2022, 5, 21, 23, 20, 52, 430, DateTimeKind.Local).AddTicks(4716) });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "ProductCategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "All tech products that you can find, from home electronics to personal gadgets.", "Electronics" },
                    { 2, "Find your prefered endergy drinks here. From energy drings to pops", "Drinks" },
                    { 3, "Lastes models of clothing. See and dont miss lastes mode", "Clothes" }
                });
        }
    }
}
