using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PiRiS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditDeposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Clients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountPlans",
                columns: table => new
                {
                    AccountPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AccountType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPlans", x => x.AccountPlanId);
                });

            migrationBuilder.CreateTable(
                name: "BankInformation",
                columns: table => new
                {
                    BankInformationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankInformation", x => x.BankInformationId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrencyName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    Debit = table.Column<decimal>(type: "numeric", nullable: false),
                    Credit = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    AccountPlanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountPlans_AccountPlanId",
                        column: x => x.AccountPlanId,
                        principalTable: "AccountPlans",
                        principalColumn: "AccountPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditPlans",
                columns: table => new
                {
                    CreditPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MonthPeriod = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Percent = table.Column<double>(type: "double precision", nullable: false),
                    CreditType = table.Column<int>(type: "integer", nullable: false),
                    MainAccountPlanId = table.Column<int>(type: "integer", nullable: false),
                    PercentAccountPlanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditPlans", x => x.CreditPlanId);
                    table.ForeignKey(
                        name: "FK_CreditPlans_AccountPlans_MainAccountPlanId",
                        column: x => x.MainAccountPlanId,
                        principalTable: "AccountPlans",
                        principalColumn: "AccountPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditPlans_AccountPlans_PercentAccountPlanId",
                        column: x => x.PercentAccountPlanId,
                        principalTable: "AccountPlans",
                        principalColumn: "AccountPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditPlans_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepositPlans",
                columns: table => new
                {
                    DepositPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    DayPeriod = table.Column<int>(type: "integer", nullable: false),
                    Percent = table.Column<double>(type: "double precision", nullable: false),
                    DepositType = table.Column<int>(type: "integer", nullable: false),
                    MainAccountPlanId = table.Column<int>(type: "integer", nullable: false),
                    PercentAccountPlanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepositPlans", x => x.DepositPlanId);
                    table.ForeignKey(
                        name: "FK_DepositPlans_AccountPlans_MainAccountPlanId",
                        column: x => x.MainAccountPlanId,
                        principalTable: "AccountPlans",
                        principalColumn: "AccountPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositPlans_AccountPlans_PercentAccountPlanId",
                        column: x => x.PercentAccountPlanId,
                        principalTable: "AccountPlans",
                        principalColumn: "AccountPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepositPlans_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DebitAccountId = table.Column<int>(type: "integer", nullable: false),
                    CreditAccountId = table.Column<int>(type: "integer", nullable: false),
                    TransactionDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_CreditAccountId",
                        column: x => x.CreditAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_DebitAccountId",
                        column: x => x.DebitAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    CreditId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreditNumber = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    CreditPlanId = table.Column<int>(type: "integer", nullable: false),
                    MainAccountId = table.Column<int>(type: "integer", nullable: false),
                    PercentAccountId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    CreditCardCode = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.CreditId);
                    table.ForeignKey(
                        name: "FK_Credits_Accounts_MainAccountId",
                        column: x => x.MainAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credits_Accounts_PercentAccountId",
                        column: x => x.PercentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credits_CreditPlans_CreditPlanId",
                        column: x => x.CreditPlanId,
                        principalTable: "CreditPlans",
                        principalColumn: "CreditPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    DepositId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepositNumber = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    MainAccountId = table.Column<int>(type: "integer", nullable: false),
                    PercentAccountId = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    DepositPlanId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.DepositId);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_MainAccountId",
                        column: x => x.MainAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_PercentAccountId",
                        column: x => x.PercentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deposits_DepositPlans_DepositPlanId",
                        column: x => x.DepositPlanId,
                        principalTable: "DepositPlans",
                        principalColumn: "DepositPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CurrencyId",
                table: "Clients",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPlans_Code",
                table: "AccountPlans",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountNumber",
                table: "Accounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountPlanId",
                table: "Accounts",
                column: "AccountPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditPlans_CurrencyId",
                table: "CreditPlans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditPlans_MainAccountPlanId",
                table: "CreditPlans",
                column: "MainAccountPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditPlans_PercentAccountPlanId",
                table: "CreditPlans",
                column: "PercentAccountPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_ClientId",
                table: "Credits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CreditCardCode",
                table: "Credits",
                column: "CreditCardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CreditCardNumber",
                table: "Credits",
                column: "CreditCardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CreditNumber",
                table: "Credits",
                column: "CreditNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CreditPlanId",
                table: "Credits",
                column: "CreditPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_MainAccountId",
                table: "Credits",
                column: "MainAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_PercentAccountId",
                table: "Credits",
                column: "PercentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPlans_CurrencyId",
                table: "DepositPlans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPlans_MainAccountPlanId",
                table: "DepositPlans",
                column: "MainAccountPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_DepositPlans_PercentAccountPlanId",
                table: "DepositPlans",
                column: "PercentAccountPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ClientId",
                table: "Deposits",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_DepositNumber",
                table: "Deposits",
                column: "DepositNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_DepositPlanId",
                table: "Deposits",
                column: "DepositPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_MainAccountId",
                table: "Deposits",
                column: "MainAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_PercentAccountId",
                table: "Deposits",
                column: "PercentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditAccountId",
                table: "Transactions",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DebitAccountId",
                table: "Transactions",
                column: "DebitAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Currencies_CurrencyId",
                table: "Clients",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Currencies_CurrencyId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "BankInformation");

            migrationBuilder.DropTable(
                name: "Credits");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "CreditPlans");

            migrationBuilder.DropTable(
                name: "DepositPlans");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "AccountPlans");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CurrencyId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Clients");
        }
    }
}
