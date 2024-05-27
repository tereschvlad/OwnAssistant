using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class AddTelegramId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "Long",
                table: "CheckpointInfoTasks",
                type: "numeric(26,20)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,20)",
                oldPrecision: 3,
                oldScale: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "CheckpointInfoTasks",
                type: "numeric(26,20)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,20)",
                oldPrecision: 3,
                oldScale: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                table: "Users");

            migrationBuilder.AlterColumn<decimal>(
                name: "Long",
                table: "CheckpointInfoTasks",
                type: "decimal(3,20)",
                precision: 3,
                scale: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(26,20)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "CheckpointInfoTasks",
                type: "decimal(3,20)",
                precision: 3,
                scale: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(26,20)");
        }
    }
}
