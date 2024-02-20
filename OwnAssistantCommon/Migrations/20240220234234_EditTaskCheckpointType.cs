using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnAssistantCommon.Migrations
{
    /// <inheritdoc />
    public partial class EditTaskCheckpointType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Long",
                table: "CheckpointInfoTasks",
                type: "decimal(3,20)",
                precision: 3,
                scale: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "CheckpointInfoTasks",
                type: "decimal(3,20)",
                precision: 3,
                scale: 20,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Long",
                table: "CheckpointInfoTasks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,20)",
                oldPrecision: 3,
                oldScale: 20);

            migrationBuilder.AlterColumn<decimal>(
                name: "Lat",
                table: "CheckpointInfoTasks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,20)",
                oldPrecision: 3,
                oldScale: 20);
        }
    }
}
