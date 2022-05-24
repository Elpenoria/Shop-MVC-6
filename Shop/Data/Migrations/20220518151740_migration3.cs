using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Data.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 18, 17, 17, 39, 315, DateTimeKind.Local).AddTicks(9160), new DateTime(2022, 5, 18, 17, 17, 39, 315, DateTimeKind.Local).AddTicks(9122) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 5, 17, 23, 6, 16, 824, DateTimeKind.Local).AddTicks(1212), new DateTime(2022, 5, 17, 23, 6, 16, 824, DateTimeKind.Local).AddTicks(1168) });
        }
    }
}
