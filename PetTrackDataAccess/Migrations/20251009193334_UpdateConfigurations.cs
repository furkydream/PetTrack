using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetTrackDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TrackerDeviceId",
                table: "ActivityLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_TrackerDeviceId",
                table: "ActivityLogs",
                column: "TrackerDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_TrackerDevices_TrackerDeviceId",
                table: "ActivityLogs",
                column: "TrackerDeviceId",
                principalTable: "TrackerDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_TrackerDevices_TrackerDeviceId",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_TrackerDeviceId",
                table: "ActivityLogs");

            migrationBuilder.DropColumn(
                name: "TrackerDeviceId",
                table: "ActivityLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
