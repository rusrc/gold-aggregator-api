using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddTranlitToCoinCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranslitName",
                table: "CoinCatalogs",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "TranslitName",
                value: "georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "TranslitName",
                value: "amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 4,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 5,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 6,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 7,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 8,
                column: "TranslitName",
                value: "kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 9,
                column: "TranslitName",
                value: "avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 10,
                column: "TranslitName",
                value: "panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 11,
                column: "TranslitName",
                value: "georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 12,
                column: "TranslitName",
                value: "amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 13,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 14,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 15,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 16,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 17,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 18,
                column: "TranslitName",
                value: "kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 19,
                column: "TranslitName",
                value: "avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 20,
                column: "TranslitName",
                value: "panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 21,
                column: "TranslitName",
                value: "georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 22,
                column: "TranslitName",
                value: "amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 23,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 24,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 25,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 26,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 27,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 28,
                column: "TranslitName",
                value: "kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 29,
                column: "TranslitName",
                value: "avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 30,
                column: "TranslitName",
                value: "panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 41,
                column: "TranslitName",
                value: "georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 42,
                column: "TranslitName",
                value: "amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 43,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 44,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 45,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 46,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 47,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 48,
                column: "TranslitName",
                value: "kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 49,
                column: "TranslitName",
                value: "avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 50,
                column: "TranslitName",
                value: "panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 51,
                column: "TranslitName",
                value: "georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 52,
                column: "TranslitName",
                value: "amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 53,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 54,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 55,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 56,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 57,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 58,
                column: "TranslitName",
                value: "kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 59,
                column: "TranslitName",
                value: "avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 60,
                column: "TranslitName",
                value: "panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 61,
                column: "TranslitName",
                value: "venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 62,
                column: "TranslitName",
                value: "avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 63,
                column: "TranslitName",
                value: "klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 64,
                column: "TranslitName",
                value: "amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 65,
                column: "TranslitName",
                value: "britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 66,
                column: "TranslitName",
                value: "utkonos");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 67,
                column: "TranslitName",
                value: "korolevskij-gerb");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 68,
                column: "TranslitName",
                value: "belaya-loshad-gannovera");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 69,
                column: "TranslitName",
                value: "belyj-grejxaund-richmonda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 70,
                column: "TranslitName",
                value: "zveri-korolevy");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 71,
                column: "TranslitName",
                value: "belyj-lev-mortimera");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 72,
                column: "TranslitName",
                value: "korolevskij-edinorog");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 73,
                column: "TranslitName",
                value: "olimpijskie-igry");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "829e0cb0-7b54-487f-a45c-f73b71a4a279");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslitName",
                table: "CoinCatalogs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "24ac1e11-4f53-4ea3-871a-e7ac7e62941c");
        }
    }
}
