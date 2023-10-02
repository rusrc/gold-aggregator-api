using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class UpdateTypesDoubleToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceToSell",
                table: "CoinsHistory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceToBuy",
                table: "CoinsHistory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceSpecial",
                table: "CoinsHistory",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceToSell",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceToBuy",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceSpecial",
                table: "CoinPrices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "CoinCatalogs",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 5,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 6,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 7,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 8,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 9,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 10,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 11,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 12,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 13,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 14,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 15,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 16,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 17,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 18,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 19,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 20,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 21,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 22,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 23,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 24,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 25,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 26,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 27,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 28,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 29,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 30,
                column: "Weight",
                value: 7.78m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 41,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 42,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 43,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 44,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 45,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 46,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 47,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 48,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 49,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 50,
                column: "Weight",
                value: 3.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 51,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 52,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 53,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 54,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 55,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 56,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 57,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 58,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 59,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 60,
                column: "Weight",
                value: 31.11m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 61,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 62,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 63,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 64,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 65,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 66,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 67,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 68,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 69,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 70,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 71,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 72,
                column: "Weight",
                value: 31.1m);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 73,
                column: "Weight",
                value: 15.55m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "eefb4d9d-e0a5-4c17-9eeb-7553288668bd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceToSell",
                table: "CoinsHistory",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PriceToBuy",
                table: "CoinsHistory",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PriceSpecial",
                table: "CoinsHistory",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PriceToSell",
                table: "CoinPrices",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PriceToBuy",
                table: "CoinPrices",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "PriceSpecial",
                table: "CoinPrices",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "CoinCatalogs",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 5,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 6,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 7,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 8,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 9,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 10,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 11,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 12,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 13,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 14,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 15,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 16,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 17,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 18,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 19,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 20,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 21,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 22,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 23,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 24,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 25,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 26,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 27,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 28,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 29,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 30,
                column: "Weight",
                value: 7.7800000000000002);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 41,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 42,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 43,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 44,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 45,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 46,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 47,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 48,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 49,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 50,
                column: "Weight",
                value: 3.1099999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 51,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 52,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 53,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 54,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 55,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 56,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 57,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 58,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 59,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 60,
                column: "Weight",
                value: 31.109999999999999);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 61,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 62,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 63,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 64,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 65,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 66,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 67,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 68,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 69,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 70,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 71,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 72,
                column: "Weight",
                value: 31.100000000000001);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 73,
                column: "Weight",
                value: 15.550000000000001);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "8a54f39e-2fd9-4612-9822-c92db13a89e1");
        }
    }
}
