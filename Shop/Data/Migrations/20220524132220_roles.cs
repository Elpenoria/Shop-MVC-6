using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CategoryId", "Description", "Discount", "EndDate", "EventHeader", "EventName", "ImageUrl", "SellerId", "StartDate" },
                values: new object[] { 1, 0, "Lorem impsum Lorem impsum Lorem impsumLorem impsumLorem impsum Lorem impsumvLorem impsum Lorem impsumLorem impsum", 10, new DateTime(2022, 5, 24, 14, 58, 6, 747, DateTimeKind.Local).AddTicks(6751), "Once a year, take your chance", "Electronics Discount", "imageName.jpg", "Test", new DateTime(2022, 5, 24, 14, 58, 6, 747, DateTimeKind.Local).AddTicks(6707) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "DiscountedPrice", "EventId", "InStock", "MainImage", "Price", "ProductName", "SecondImage", "SellerId", "ThirdImage" },
                values: new object[] { 1, 2, "Taste the feeling or sth like that. ", 5m, null, 1000, "drinkBlue1.jpg", 5m, "drinkBlue", null, "abcd", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "DiscountedPrice", "EventId", "InStock", "MainImage", "Price", "ProductName", "SecondImage", "SellerId", "ThirdImage" },
                values: new object[] { 2, 2, "Tonic afert long night drinking ", 3m, null, 300, "drinkTonic1.jpg", 3m, "drinkTonic", null, "abcd", null });
        }
    }
}
