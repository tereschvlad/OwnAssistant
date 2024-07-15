using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderTime1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendingTime",
                table: "MainInfoTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SendingTime",
                table: "MainInfoTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
