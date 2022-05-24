using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class imgaUploadtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "MainImage");

            migrationBuilder.AddColumn<string>(
                name: "SecondImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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
                values: new object[] { new DateTime(2022, 5, 21, 9, 16, 20, 641, DateTimeKind.Local).AddTicks(6054), new DateTime(2022, 5, 21, 9, 16, 20, 641, DateTimeKind.Local).AddTicks(6017) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ThirdImage",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "MainImage",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 19, 22, 55, 35, 328, DateTimeKind.Local).AddTicks(826), new DateTime(2022, 5, 19, 22, 55, 35, 328, DateTimeKind.Local).AddTicks(782) });
        }
    }
}
