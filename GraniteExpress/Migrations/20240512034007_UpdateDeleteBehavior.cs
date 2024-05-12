using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails",
                column: "JournalId",
                principalTable: "GenJournal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails",
                column: "JournalId",
                principalTable: "GenJournal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
