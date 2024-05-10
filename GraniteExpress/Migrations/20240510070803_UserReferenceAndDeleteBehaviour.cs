using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class UserReferenceAndDeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefClient_ClientId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefDocumentType_DocumentTypeId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefTemplate_TemplateId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_RefAccount_AccountId",
                table: "GenJournalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RefAccount_RefAccountType_AccountTypeId",
                table: "RefAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_RefAccount_RefCurrency_CurrencyId",
                table: "RefAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_RefClient_Clientype_ClientTypeId",
                table: "RefClient");

            migrationBuilder.DropForeignKey(
                name: "FK_RefTemplateDetails_RefAccount_AccountId",
                table: "RefTemplateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RefTemplateDetails_RefTemplate_TemplateId",
                table: "RefTemplateDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GenJournal",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournal_UserId",
                table: "GenJournal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_AspNetUsers_UserId",
                table: "GenJournal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefClient_ClientId",
                table: "GenJournal",
                column: "ClientId",
                principalTable: "RefClient",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefDocumentType_DocumentTypeId",
                table: "GenJournal",
                column: "DocumentTypeId",
                principalTable: "RefDocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefTemplate_TemplateId",
                table: "GenJournal",
                column: "TemplateId",
                principalTable: "RefTemplate",
                principalColumn: "TemplateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails",
                column: "JournalId",
                principalTable: "GenJournal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_RefAccount_AccountId",
                table: "GenJournalDetails",
                column: "AccountId",
                principalTable: "RefAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefAccount_RefAccountType_AccountTypeId",
                table: "RefAccount",
                column: "AccountTypeId",
                principalTable: "RefAccountType",
                principalColumn: "AccountTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefAccount_RefCurrency_CurrencyId",
                table: "RefAccount",
                column: "CurrencyId",
                principalTable: "RefCurrency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefClient_Clientype_ClientTypeId",
                table: "RefClient",
                column: "ClientTypeId",
                principalTable: "Clientype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefTemplateDetails_RefAccount_AccountId",
                table: "RefTemplateDetails",
                column: "AccountId",
                principalTable: "RefAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefTemplateDetails_RefTemplate_TemplateId",
                table: "RefTemplateDetails",
                column: "TemplateId",
                principalTable: "RefTemplate",
                principalColumn: "TemplateId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_AspNetUsers_UserId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefClient_ClientId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefDocumentType_DocumentTypeId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournal_RefTemplate_TemplateId",
                table: "GenJournal");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_GenJournalDetails_RefAccount_AccountId",
                table: "GenJournalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RefAccount_RefAccountType_AccountTypeId",
                table: "RefAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_RefAccount_RefCurrency_CurrencyId",
                table: "RefAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_RefClient_Clientype_ClientTypeId",
                table: "RefClient");

            migrationBuilder.DropForeignKey(
                name: "FK_RefTemplateDetails_RefAccount_AccountId",
                table: "RefTemplateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_RefTemplateDetails_RefTemplate_TemplateId",
                table: "RefTemplateDetails");

            migrationBuilder.DropIndex(
                name: "IX_GenJournal_UserId",
                table: "GenJournal");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GenJournal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefClient_ClientId",
                table: "GenJournal",
                column: "ClientId",
                principalTable: "RefClient",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefDocumentType_DocumentTypeId",
                table: "GenJournal",
                column: "DocumentTypeId",
                principalTable: "RefDocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournal_RefTemplate_TemplateId",
                table: "GenJournal",
                column: "TemplateId",
                principalTable: "RefTemplate",
                principalColumn: "TemplateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_GenJournal_JournalId",
                table: "GenJournalDetails",
                column: "JournalId",
                principalTable: "GenJournal",
                principalColumn: "JournalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenJournalDetails_RefAccount_AccountId",
                table: "GenJournalDetails",
                column: "AccountId",
                principalTable: "RefAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefAccount_RefAccountType_AccountTypeId",
                table: "RefAccount",
                column: "AccountTypeId",
                principalTable: "RefAccountType",
                principalColumn: "AccountTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefAccount_RefCurrency_CurrencyId",
                table: "RefAccount",
                column: "CurrencyId",
                principalTable: "RefCurrency",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefClient_Clientype_ClientTypeId",
                table: "RefClient",
                column: "ClientTypeId",
                principalTable: "Clientype",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefTemplateDetails_RefAccount_AccountId",
                table: "RefTemplateDetails",
                column: "AccountId",
                principalTable: "RefAccount",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefTemplateDetails_RefTemplate_TemplateId",
                table: "RefTemplateDetails",
                column: "TemplateId",
                principalTable: "RefTemplate",
                principalColumn: "TemplateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
