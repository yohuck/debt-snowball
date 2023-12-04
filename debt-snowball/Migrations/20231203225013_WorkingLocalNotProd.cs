using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace debtsnowball.Migrations
{
    /// <inheritdoc />
    public partial class WorkingLocalNotProd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Snowballs_SnowballaSnowballId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_Snowballs_AspNetUsers_UserId",
                table: "Snowballs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Snowballs",
                table: "Snowballs");

            migrationBuilder.RenameTable(
                name: "Snowballs",
                newName: "Snowballas");

            migrationBuilder.RenameIndex(
                name: "IX_Snowballs_UserId",
                table: "Snowballas",
                newName: "IX_Snowballas_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Snowballas",
                table: "Snowballas",
                column: "SnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Snowballas_SnowballaSnowballId",
                table: "Debts",
                column: "SnowballaSnowballId",
                principalTable: "Snowballas",
                principalColumn: "SnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Snowballas_AspNetUsers_UserId",
                table: "Snowballas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Snowballas_SnowballaSnowballId",
                table: "Debts");

            migrationBuilder.DropForeignKey(
                name: "FK_Snowballas_AspNetUsers_UserId",
                table: "Snowballas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Snowballas",
                table: "Snowballas");

            migrationBuilder.RenameTable(
                name: "Snowballas",
                newName: "Snowballs");

            migrationBuilder.RenameIndex(
                name: "IX_Snowballas_UserId",
                table: "Snowballs",
                newName: "IX_Snowballs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Snowballs",
                table: "Snowballs",
                column: "SnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Snowballs_SnowballaSnowballId",
                table: "Debts",
                column: "SnowballaSnowballId",
                principalTable: "Snowballs",
                principalColumn: "SnowballId");

            migrationBuilder.AddForeignKey(
                name: "FK_Snowballs_AspNetUsers_UserId",
                table: "Snowballs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
