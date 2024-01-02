using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class ReCreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskUser");

            migrationBuilder.CreateTable(
                name: "CustomerTaskUser",
                columns: table => new
                {
                    PerformingTasksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformingUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTaskUser", x => new { x.PerformingTasksId, x.PerformingUsersId });
                    table.ForeignKey(
                        name: "FK_CustomerTaskUser_Tasks_PerformingTasksId",
                        column: x => x.PerformingTasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerTaskUser_Users_PerformingUsersId",
                        column: x => x.PerformingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTaskUser_PerformingUsersId",
                table: "CustomerTaskUser",
                column: "PerformingUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTaskUser");

            migrationBuilder.CreateTable(
                name: "TaskUser",
                columns: table => new
                {
                    PerformingTasksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformingUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUser", x => new { x.PerformingTasksId, x.PerformingUsersId });
                    table.ForeignKey(
                        name: "FK_TaskUser_Tasks_PerformingTasksId",
                        column: x => x.PerformingTasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUser_Users_PerformingUsersId",
                        column: x => x.PerformingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskUser_PerformingUsersId",
                table: "TaskUser",
                column: "PerformingUsersId");
        }
    }
}
