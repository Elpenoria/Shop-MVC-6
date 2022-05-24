using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 24, 14, 58, 6, 747, DateTimeKind.Local).AddTicks(6751), new DateTime(2022, 5, 24, 14, 58, 6, 747, DateTimeKind.Local).AddTicks(6707) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 24, 11, 24, 21, 636, DateTimeKind.Local).AddTicks(2107), new DateTime(2022, 5, 24, 11, 24, 21, 636, DateTimeKind.Local).AddTicks(2066) });
        }
    }
}
