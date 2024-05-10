using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class addedCashFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefCashFlow",
                columns: table => new
                {
                    CashFlowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CashFlowDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCashFlow", x => x.CashFlowId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenJournalDetails_CashFlowId",
                table: "GenJournalDetails",
                column: "CashFlowId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_RefCashFlow_CashFlowId",
                table: "GenJournalDetails",
                column: "CashFlowId",
                principalTable: "RefCashFlow",
                principalColumn: "CashFlowId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_RefCashFlow_CashFlowId",
                table: "GenJournalDetails");

            migrationBuilder.DropTable(
                name: "RefCashFlow");

            migrationBuilder.DropIndex(
                name: "IX_GenJournalDetails_CashFlowId",
                table: "GenJournalDetails");
        }
    }
}
