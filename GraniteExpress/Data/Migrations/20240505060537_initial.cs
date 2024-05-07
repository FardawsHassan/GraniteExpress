using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraniteExpress.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientype",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefAccountType",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    BalanceId = table.Column<int>(type: "int", nullable: true),
                    CashId = table.Column<int>(type: "int", nullable: true),
                    IncomeId = table.Column<int>(type: "int", nullable: true),
                    EquityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAccountType", x => x.AccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefCurrency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultValue = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefCurrency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "RefDocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentMaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefDocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RefTemplate",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTemplate", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "RefClient",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientTypeId = table.Column<int>(type: "int", nullable: false),
                    ClientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefClient", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_RefClient_Clientype_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "Clientype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefAccount",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountTypeId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefAccount", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_RefAccount_RefAccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "RefAccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefAccount_RefCurrency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "RefCurrency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenJournal",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    JournalDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DocumentNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenJournal", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_GenJournal_RefClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "RefClient",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenJournal_RefDocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "RefDocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenJournal_RefTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "RefTemplate",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefTemplateDetails",
                columns: table => new
                {
                    TemplateDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefTemplateDetails", x => x.TemplateDetailsId);
                    table.ForeignKey(
                        name: "FK_RefTemplateDetails_RefAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "RefAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RefTemplateDetails_RefClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "RefClient",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_RefTemplateDetails_RefTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "RefTemplate",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenJournalDetails",
                columns: table => new
                {
                    JournalDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    IsDebit = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CurrencyAmount = table.Column<decimal>(type: "decimal(30,6)", precision: 30, scale: 6, nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(30,6)", precision: 30, scale: 6, nullable: true),
                    CashFlowId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenJournalDetails", x => x.JournalDetailId);
                    table.ForeignKey(
                        name: "FK_GenJournalDetails_GenJournal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "GenJournal",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenJournalDetails_RefAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "RefAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenJournalDetails_RefClient_ClientId",
                        column: x => x.ClientId,
                        principalTable: "RefClient",
                        principalColumn: "ClientId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenJournal_ClientId",
                table: "GenJournal",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournal_DocumentTypeId",
                table: "GenJournal",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournal_TemplateId",
                table: "GenJournal",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournalDetails_AccountId",
                table: "GenJournalDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournalDetails_ClientId",
                table: "GenJournalDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_GenJournalDetails_JournalId",
                table: "GenJournalDetails",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_RefAccount_AccountTypeId",
                table: "RefAccount",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefAccount_CurrencyId",
                table: "RefAccount",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RefClient_ClientTypeId",
                table: "RefClient",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefTemplateDetails_AccountId",
                table: "RefTemplateDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RefTemplateDetails_ClientId",
                table: "RefTemplateDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RefTemplateDetails_TemplateId",
                table: "RefTemplateDetails",
                column: "TemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenJournalDetails");

            migrationBuilder.DropTable(
                name: "RefTemplateDetails");

            migrationBuilder.DropTable(
                name: "GenJournal");

            migrationBuilder.DropTable(
                name: "RefAccount");

            migrationBuilder.DropTable(
                name: "RefClient");

            migrationBuilder.DropTable(
                name: "RefDocumentType");

            migrationBuilder.DropTable(
                name: "RefTemplate");

            migrationBuilder.DropTable(
                name: "RefAccountType");

            migrationBuilder.DropTable(
                name: "RefCurrency");

            migrationBuilder.DropTable(
                name: "Clientype");
        }
    }
}
