using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace debtsnowball.Migrations
{
    /// <inheritdoc />
    public partial class ChangesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Snowballs_SnowballId",
                table: "Debts");

            migrationBuilder.RenameColumn(
                name: "SnowballId",
                table: "Debts",
                newName: "SnowballaSnowballId");

            migrationBuilder.RenameIndex(
                name: "IX_Debts_SnowballId",
                table: "Debts",
                newName: "IX_Debts_SnowballaSnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Snowballs_SnowballaSnowballId",
                table: "Debts",
                column: "SnowballaSnowballId",
                principalTable: "Snowballs",
                principalColumn: "SnowballId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Snowballs_SnowballaSnowballId",
                table: "Debts");

            migrationBuilder.RenameColumn(
                name: "SnowballaSnowballId",
                table: "Debts",
                newName: "SnowballId");

            migrationBuilder.RenameIndex(
                name: "IX_Debts_SnowballaSnowballId",
                table: "Debts",
                newName: "IX_Debts_SnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Snowballs_SnowballId",
                table: "Debts",
                column: "SnowballId",
                principalTable: "Snowballs",
                principalColumn: "SnowballId");
        }
    }
}
