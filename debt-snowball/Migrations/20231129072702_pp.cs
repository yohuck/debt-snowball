using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace debtsnowball.Migrations
{
    /// <inheritdoc />
    public partial class pp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Debts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_UserId",
                table: "Debts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_AspNetUsers_UserId",
                table: "Debts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_AspNetUsers_UserId",
                table: "Debts");

            migrationBuilder.DropIndex(
                name: "IX_Debts_UserId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Debts");
        }
    }
}
