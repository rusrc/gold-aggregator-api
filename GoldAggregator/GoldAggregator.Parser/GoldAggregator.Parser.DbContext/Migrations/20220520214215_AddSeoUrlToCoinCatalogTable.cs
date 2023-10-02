using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddSeoUrlToCoinCatalogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoUrl",
                table: "CoinCatalogs",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "SeoUrl",
                value: "zolotaya-moneta_georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "SeoUrl",
                value: "zolotaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 4,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 5,
                column: "SeoUrl",
                value: "zolotaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 6,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 7,
                column: "SeoUrl",
                value: "zolotaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 8,
                column: "SeoUrl",
                value: "zolotaya-moneta_kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 9,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 10,
                column: "SeoUrl",
                value: "zolotaya-moneta_panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 11,
                column: "SeoUrl",
                value: "zolotaya-moneta_georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 12,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 13,
                column: "SeoUrl",
                value: "zolotaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 14,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 15,
                column: "SeoUrl",
                value: "zolotaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 16,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 17,
                column: "SeoUrl",
                value: "zolotaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 18,
                column: "SeoUrl",
                value: "zolotaya-moneta_kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 19,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 20,
                column: "SeoUrl",
                value: "zolotaya-moneta_panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 21,
                column: "SeoUrl",
                value: "zolotaya-moneta_georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 22,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 23,
                column: "SeoUrl",
                value: "zolotaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 24,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 25,
                column: "SeoUrl",
                value: "zolotaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 26,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 27,
                column: "SeoUrl",
                value: "zolotaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 28,
                column: "SeoUrl",
                value: "zolotaya-moneta_kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 29,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 30,
                column: "SeoUrl",
                value: "zolotaya-moneta_panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 41,
                column: "SeoUrl",
                value: "zolotaya-moneta_georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 42,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 43,
                column: "SeoUrl",
                value: "zolotaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 44,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 45,
                column: "SeoUrl",
                value: "zolotaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 46,
                column: "SeoUrl",
                value: "zolotaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 47,
                column: "SeoUrl",
                value: "zolotaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 48,
                column: "SeoUrl",
                value: "zolotaya-moneta_kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 49,
                column: "SeoUrl",
                value: "zolotaya-moneta_avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 50,
                column: "SeoUrl",
                value: "zolotaya-moneta_panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 51,
                column: "SeoUrl",
                value: "serebryanaya-moneta_georgij-pobedonosec");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 52,
                column: "SeoUrl",
                value: "serebryanaya-moneta_amerikanskij-buffalo");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 53,
                column: "SeoUrl",
                value: "serebryanaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 54,
                column: "SeoUrl",
                value: "serebryanaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 55,
                column: "SeoUrl",
                value: "serebryanaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 56,
                column: "SeoUrl",
                value: "serebryanaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 57,
                column: "SeoUrl",
                value: "serebryanaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 58,
                column: "SeoUrl",
                value: "serebryanaya-moneta_kryugerrand");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 59,
                column: "SeoUrl",
                value: "serebryanaya-moneta_avstrallijskij-lunar");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 60,
                column: "SeoUrl",
                value: "serebryanaya-moneta_panda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 61,
                column: "SeoUrl",
                value: "platinovaya-moneta_venskij-filarmoniker");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 62,
                column: "SeoUrl",
                value: "platinovaya-moneta_avstrallijskij-kenguru");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 63,
                column: "SeoUrl",
                value: "platinovaya-moneta_klenovyj-list");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 64,
                column: "SeoUrl",
                value: "platinovaya-moneta_amerikanskij-orel");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 65,
                column: "SeoUrl",
                value: "platinovaya-moneta_britaniya");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 66,
                column: "SeoUrl",
                value: "platinovaya-moneta_utkonos");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 67,
                column: "SeoUrl",
                value: "platinovaya-moneta_korolevskij-gerb");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 68,
                column: "SeoUrl",
                value: "platinovaya-moneta_belaya-loshad-gannovera");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 69,
                column: "SeoUrl",
                value: "platinovaya-moneta_belyj-grejxaund-richmonda");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 70,
                column: "SeoUrl",
                value: "platinovaya-moneta_zveri-korolevy");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 71,
                column: "SeoUrl",
                value: "platinovaya-moneta_belyj-lev-mortimera");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 72,
                column: "SeoUrl",
                value: "platinovaya-moneta_korolevskij-edinorog");

            migrationBuilder.UpdateData(
                table: "CoinCatalogs",
                keyColumn: "Id",
                keyValue: 73,
                column: "SeoUrl",
                value: "platinovaya-moneta_olimpijskie-igry");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "8a54f39e-2fd9-4612-9822-c92db13a89e1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoUrl",
                table: "CoinCatalogs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "e408601a-8c8b-4fff-911e-ffcd45374e66");
        }
    }
}
