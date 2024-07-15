using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderTime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnRegularSending",
                table: "MainInfoTasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsSendedAlert",
                table: "DateInfoTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSendedAlert",
                table: "DateInfoTasks");

            migrationBuilder.AddColumn<bool>(
                name: "OnRegularSending",
                table: "MainInfoTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
