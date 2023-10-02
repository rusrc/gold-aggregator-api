using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddPricePerGram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriceSpecialDetails",
                table: "CoinPrices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSpecialPerGram",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToBuyPerGram",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToSellPerGram",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "90702784-4b11-4b0f-b905-ae533730f3cd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceSpecialDetails",
                table: "CoinPrices");

            migrationBuilder.DropColumn(
                name: "PriceSpecialPerGram",
                table: "CoinPrices");

            migrationBuilder.DropColumn(
                name: "PriceToBuyPerGram",
                table: "CoinPrices");

            migrationBuilder.DropColumn(
                name: "PriceToSellPerGram",
                table: "CoinPrices");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "adf6b898-c93a-4cb2-86ca-e423fcedd7b1");
        }
    }
}
