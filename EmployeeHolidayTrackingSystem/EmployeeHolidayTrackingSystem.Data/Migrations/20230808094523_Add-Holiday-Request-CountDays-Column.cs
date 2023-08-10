using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHolidayTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddHolidayRequestCountDaysColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HolidayRequestCountDays",
                table: "HolidayRequests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayRequestCountDays",
                table: "HolidayRequests");
        }
    }
}
