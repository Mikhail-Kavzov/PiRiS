using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiRiS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_HomePhone",
                table: "Clients",
                column: "HomePhone");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IdentificationNumber",
                table: "Clients",
                column: "IdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MobilePhone",
                table: "Clients",
                column: "MobilePhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportNumber_PassportSeries",
                table: "Clients",
                columns: new[] { "PassportNumber", "PassportSeries" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Surname_FirstName_LastName",
                table: "Clients",
                columns: new[] { "Surname", "FirstName", "LastName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_HomePhone",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_IdentificationNumber",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_MobilePhone",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PassportNumber_PassportSeries",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Surname_FirstName_LastName",
                table: "Clients");
        }
    }
}
