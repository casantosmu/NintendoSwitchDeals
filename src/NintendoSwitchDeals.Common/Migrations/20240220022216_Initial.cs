using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NintendoSwitchDeals.Common.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deals",
                columns: table => new
                {
                    DealId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NintendoId = table.Column<long>(type: "INTEGER", nullable: false),
                    GameName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ThresholdPrice = table.Column<double>(type: "REAL", precision: 6, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deals", x => x.DealId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deals_NintendoId",
                table: "Deals",
                column: "NintendoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deals");
        }
    }
}
