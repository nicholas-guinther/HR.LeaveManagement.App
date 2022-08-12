using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    public partial class SeedLeaveTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DefaultDays", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2022, 8, 12, 15, 40, 28, 605, DateTimeKind.Utc).AddTicks(1030), 10, "Admin", new DateTime(2022, 8, 12, 15, 40, 28, 605, DateTimeKind.Utc).AddTicks(1030), "Vacation" },
                    { 2, "Admin", new DateTime(2022, 8, 12, 15, 40, 28, 605, DateTimeKind.Utc).AddTicks(1030), 12, "Admin", new DateTime(2022, 8, 12, 15, 40, 28, 605, DateTimeKind.Utc).AddTicks(1030), "Sick" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
