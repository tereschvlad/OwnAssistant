using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLocNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "CheckpointInfoTasks");

            migrationBuilder.DropColumn(
                name: "TaskDate",
                table: "CheckpointInfoTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CheckpointInfoTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskDate",
                table: "CheckpointInfoTasks",
                type: "datetime2",
                nullable: true);
        }
    }
}
