using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class ChengeDBStructureTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTaskUser");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.CreateTable(
                name: "MainInfoTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PerformerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainInfoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainInfoTasks_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MainInfoTasks_Users_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckpointInfoTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerTaskMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Long = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckpointInfoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckpointInfoTasks_MainInfoTasks_CustomerTaskMainId",
                        column: x => x.CustomerTaskMainId,
                        principalTable: "MainInfoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DateInfoTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerTaskMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateInfoTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateInfoTasks_MainInfoTasks_CustomerTaskMainId",
                        column: x => x.CustomerTaskMainId,
                        principalTable: "MainInfoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckpointInfoTasks_CustomerTaskMainId",
                table: "CheckpointInfoTasks",
                column: "CustomerTaskMainId");

            migrationBuilder.CreateIndex(
                name: "IX_DateInfoTasks_CustomerTaskMainId",
                table: "DateInfoTasks",
                column: "CustomerTaskMainId");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfoTasks_CreatorId",
                table: "MainInfoTasks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MainInfoTasks_PerformerId",
                table: "MainInfoTasks",
                column: "PerformerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckpointInfoTasks");

            migrationBuilder.DropTable(
                name: "DateInfoTasks");

            migrationBuilder.DropTable(
                name: "MainInfoTasks");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CrtDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                column: "CreatorId");
        }
    }
}
