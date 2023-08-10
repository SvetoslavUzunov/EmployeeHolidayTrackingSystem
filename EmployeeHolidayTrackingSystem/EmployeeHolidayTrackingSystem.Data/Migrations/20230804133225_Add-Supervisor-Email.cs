using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHolidayTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayRequests_HolidayRequestStatuses_HolidayRequestStatusId",
                table: "HolidayRequests");

            migrationBuilder.DropTable(
                name: "HolidayRequestStatuses");

            migrationBuilder.DropTable(
                name: "UserHolidayRequestEntity");

            migrationBuilder.RenameColumn(
                name: "HolidayRequestStatusId",
                table: "HolidayRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayRequests_HolidayRequestStatusId",
                table: "HolidayRequests",
                newName: "IX_HolidayRequests_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorDisapprovedComment",
                table: "HolidayRequests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HolidayRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "HolidayRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorEmailAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayRequests_AspNetUsers_UserId",
                table: "HolidayRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HolidayRequests_AspNetUsers_UserId",
                table: "HolidayRequests");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HolidayRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HolidayRequests");

            migrationBuilder.DropColumn(
                name: "SupervisorEmailAddress",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HolidayRequests",
                newName: "HolidayRequestStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_HolidayRequests_UserId",
                table: "HolidayRequests",
                newName: "IX_HolidayRequests_HolidayRequestStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorDisapprovedComment",
                table: "HolidayRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "HolidayRequestStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayRequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserHolidayRequestEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HolidayRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHolidayRequestEntity", x => new { x.UserId, x.HolidayRequestId });
                    table.ForeignKey(
                        name: "FK_UserHolidayRequestEntity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHolidayRequestEntity_HolidayRequests_HolidayRequestId",
                        column: x => x.HolidayRequestId,
                        principalTable: "HolidayRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHolidayRequestEntity_HolidayRequestId",
                table: "UserHolidayRequestEntity",
                column: "HolidayRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_HolidayRequests_HolidayRequestStatuses_HolidayRequestStatusId",
                table: "HolidayRequests",
                column: "HolidayRequestStatusId",
                principalTable: "HolidayRequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
