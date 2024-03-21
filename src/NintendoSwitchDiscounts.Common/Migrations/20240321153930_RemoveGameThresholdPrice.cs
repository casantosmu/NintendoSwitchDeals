using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NintendoSwitchDiscounts.Common.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGameThresholdPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThresholdPrice",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ThresholdPrice",
                table: "Games",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
