using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 18, 20, 12, 20, 64, DateTimeKind.Local).AddTicks(8150), new DateTime(2022, 5, 18, 20, 12, 20, 64, DateTimeKind.Local).AddTicks(8112) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 18, 19, 54, 24, 542, DateTimeKind.Local).AddTicks(9342), new DateTime(2022, 5, 18, 19, 54, 24, 542, DateTimeKind.Local).AddTicks(9304) });
        }
    }
}
