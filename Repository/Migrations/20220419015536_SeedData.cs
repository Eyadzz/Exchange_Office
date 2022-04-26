using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistories_CurrencyId",
                table: "ExchangeHistories",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeHistories_Currencies_CurrencyId",
                table: "ExchangeHistories",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeHistories_Currencies_CurrencyId",
                table: "ExchangeHistories");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeHistories_CurrencyId",
                table: "ExchangeHistories");
        }
    }
}
