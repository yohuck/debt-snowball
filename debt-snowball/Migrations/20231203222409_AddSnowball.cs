using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace debtsnowball.Migrations
{
    /// <inheritdoc />
    public partial class AddSnowball : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SnowballId",
                table: "Debts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Snowballs",
                columns: table => new
                {
                    SnowballId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExtraPayment = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snowballs", x => x.SnowballId);
                    table.ForeignKey(
                        name: "FK_Snowballs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_SnowballId",
                table: "Debts",
                column: "SnowballId");

            migrationBuilder.CreateIndex(
                name: "IX_Snowballs_UserId",
                table: "Snowballs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Debts_Snowballs_SnowballId",
                table: "Debts",
                column: "SnowballId",
                principalTable: "Snowballs",
                principalColumn: "SnowballId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Debts_Snowballs_SnowballId",
                table: "Debts");

            migrationBuilder.DropTable(
                name: "Snowballs");

            migrationBuilder.DropIndex(
                name: "IX_Debts_SnowballId",
                table: "Debts");

            migrationBuilder.DropColumn(
                name: "SnowballId",
                table: "Debts");
        }
    }
}
