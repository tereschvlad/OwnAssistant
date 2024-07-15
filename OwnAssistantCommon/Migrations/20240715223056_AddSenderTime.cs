using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnRegularSending",
                table: "MainInfoTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendingTime",
                table: "MainInfoTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TaskDate",
                table: "DateInfoTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnRegularSending",
                table: "MainInfoTasks");

            migrationBuilder.DropColumn(
                name: "SendingTime",
                table: "MainInfoTasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TaskDate",
                table: "DateInfoTasks",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
