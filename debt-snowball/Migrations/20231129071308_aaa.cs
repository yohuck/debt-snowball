using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace debtsnowball.Migrations
{
    /// <inheritdoc />
    public partial class aaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Table_DebtTable_DebtId",
                table: "Payment_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment_Table",
                table: "Payment_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebtTable",
                table: "DebtTable");

            migrationBuilder.RenameTable(
                name: "Payment_Table",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "DebtTable",
                newName: "Debts");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_Table_DebtId",
                table: "Payments",
                newName: "IX_Payments_DebtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Debts",
                table: "Debts",
                column: "DebtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Debts_DebtId",
                table: "Payments",
                column: "DebtId",
                principalTable: "Debts",
                principalColumn: "DebtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Debts_DebtId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Debts",
                table: "Debts");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment_Table");

            migrationBuilder.RenameTable(
                name: "Debts",
                newName: "DebtTable");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_DebtId",
                table: "Payment_Table",
                newName: "IX_Payment_Table_DebtId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment_Table",
                table: "Payment_Table",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebtTable",
                table: "DebtTable",
                column: "DebtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Table_DebtTable_DebtId",
                table: "Payment_Table",
                column: "DebtId",
                principalTable: "DebtTable",
                principalColumn: "DebtId");
        }
    }
}
