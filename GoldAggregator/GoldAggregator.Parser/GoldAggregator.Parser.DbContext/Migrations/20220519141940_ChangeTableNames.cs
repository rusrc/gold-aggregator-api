using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinsHistory_Catalogs_CoinFromCatalogId",
                table: "CoinsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_DillerCoinMaps_Catalogs_CoinFromCatalogId",
                table: "DillerCoinMaps");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.CreateTable(
                name: "MintCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MintCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinCatalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Nomination = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    MintCountryId = table.Column<int>(type: "integer", nullable: false),
                    MetalType = table.Column<int>(type: "integer", nullable: false),
                    StartMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinCatalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinCatalogs_MintCountries_MintCountryId",
                        column: x => x.MintCountryId,
                        principalTable: "MintCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    PriceToBuy = table.Column<double>(type: "double precision", nullable: false),
                    PriceToSell = table.Column<double>(type: "double precision", nullable: false),
                    PriceSpecial = table.Column<double>(type: "double precision", nullable: false),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: true),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinPrices_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinPrices_CoinCatalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "CoinCatalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinPrices_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MintCountries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Австралия" },
                    { 2, "Австрия" },
                    { 3, "Армения" },
                    { 4, "Беларусь" },
                    { 5, "Великобритания" },
                    { 6, "Германия" },
                    { 7, "Казахстан" },
                    { 8, "Камерун" },
                    { 9, "Канада" },
                    { 10, "Китай" },
                    { 11, "Конго" },
                    { 12, "Кыргыстан" },
                    { 13, "Либерия" },
                    { 14, "Малави" },
                    { 15, "Мексика" },
                    { 16, "Монголия" },
                    { 17, "Науру" },
                    { 18, "Ниуэ" },
                    { 19, "Острова Мэн" },
                    { 20, "Острова Кука" },
                    { 21, "Палау" },
                    { 22, "Россия" },
                    { 23, "Руанда" },
                    { 24, "Сент-Китс И Невис" },
                    { 25, "Соломоновы Острова" },
                    { 26, "Сомали" },
                    { 27, "США" },
                    { 28, "СССР" },
                    { 29, "Тувалу" },
                    { 30, "Украина" },
                    { 31, "Фиджи" },
                    { 32, "Франция" },
                    { 33, "Швейцария" },
                    { 34, "ЮАР" },
                    { 35, "Южная Корея" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "24ac1e11-4f53-4ea3-871a-e7ac7e62941c");

            migrationBuilder.InsertData(
                table: "CoinCatalogs",
                columns: new[] { "Id", "EndMiningYear", "MetalType", "MintCountryId", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[,]
                {
                    { 1, null, 1, 22, "Георгий Победоносец", "200 рублей", null, 31.100000000000001 },
                    { 2, null, 1, 27, "Американский Буффало", null, null, 31.100000000000001 },
                    { 3, null, 1, 2, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 4, null, 1, 1, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 5, null, 1, 9, "Кленовый лист", null, null, 31.100000000000001 },
                    { 6, null, 1, 27, "Американский Орел", null, null, 31.100000000000001 },
                    { 7, null, 1, 5, "Британия", null, null, 31.100000000000001 },
                    { 8, null, 1, 34, "Крюгерранд", null, null, 31.100000000000001 },
                    { 9, null, 1, 1, "Австраллийский Лунар", null, null, 31.100000000000001 },
                    { 10, null, 1, 10, "Панда", null, null, 31.100000000000001 },
                    { 11, null, 1, 22, "Георгий Победоносец", null, null, 15.550000000000001 },
                    { 12, null, 1, 27, "Американский Буффало", null, null, 15.550000000000001 },
                    { 13, null, 1, 2, "Венский Филармоникер", null, null, 15.550000000000001 },
                    { 14, null, 1, 1, "Австраллийский Кенгуру", null, null, 15.550000000000001 },
                    { 15, null, 1, 9, "Кленовый лист", null, null, 15.550000000000001 },
                    { 16, null, 1, 27, "Американский Орел", null, null, 15.550000000000001 },
                    { 17, null, 1, 5, "Британия", null, null, 15.550000000000001 },
                    { 18, null, 1, 34, "Крюгерранд", null, null, 15.550000000000001 },
                    { 19, null, 1, 1, "Австраллийский Лунар", null, null, 15.550000000000001 },
                    { 20, null, 1, 10, "Панда", null, null, 15.550000000000001 },
                    { 21, null, 1, 22, "Георгий Победоносец", null, null, 7.7800000000000002 },
                    { 22, null, 1, 27, "Американский Буффало", null, null, 7.7800000000000002 },
                    { 23, null, 1, 2, "Венский Филармоникер", null, null, 7.7800000000000002 },
                    { 24, null, 1, 1, "Австраллийский Кенгуру", null, null, 7.7800000000000002 },
                    { 25, null, 1, 9, "Кленовый лист", null, null, 7.7800000000000002 },
                    { 26, null, 1, 27, "Американский Орел", null, null, 7.7800000000000002 },
                    { 27, null, 1, 5, "Британия", null, null, 7.7800000000000002 },
                    { 28, null, 1, 34, "Крюгерранд", null, null, 7.7800000000000002 },
                    { 29, null, 1, 1, "Австраллийский Лунар", null, null, 7.7800000000000002 },
                    { 30, null, 1, 10, "Панда", null, null, 7.7800000000000002 },
                    { 41, null, 1, 22, "Георгий Победоносец", null, null, 3.1099999999999999 },
                    { 42, null, 1, 27, "Американский Буффало", null, null, 3.1099999999999999 },
                    { 43, null, 1, 2, "Венский Филармоникер", null, null, 3.1099999999999999 },
                    { 44, null, 1, 1, "Австраллийский Кенгуру", null, null, 3.1099999999999999 },
                    { 45, null, 1, 9, "Кленовый лист", null, null, 3.1099999999999999 },
                    { 46, null, 1, 27, "Американский Орел", null, null, 3.1099999999999999 },
                    { 47, null, 1, 5, "Британия", null, null, 3.1099999999999999 },
                    { 48, null, 1, 34, "Крюгерранд", null, null, 3.1099999999999999 },
                    { 49, null, 1, 1, "Австраллийский Лунар", null, null, 3.1099999999999999 },
                    { 50, null, 1, 10, "Панда", null, null, 3.1099999999999999 },
                    { 51, null, 2, 22, "Георгий Победоносец", null, null, 31.100000000000001 },
                    { 52, null, 2, 27, "Американский Буффало", null, null, 31.100000000000001 },
                    { 53, null, 2, 2, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 54, null, 2, 1, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 55, null, 2, 9, "Кленовый лист", null, null, 31.100000000000001 },
                    { 56, null, 2, 27, "Американский Орел", null, null, 31.100000000000001 },
                    { 57, null, 2, 5, "Британия", null, null, 31.100000000000001 },
                    { 58, null, 2, 34, "Крюгерранд", null, null, 31.100000000000001 },
                    { 59, null, 2, 1, "Австраллийский Лунар", null, null, 31.100000000000001 },
                    { 60, null, 2, 10, "Панда", null, null, 31.109999999999999 },
                    { 61, null, 3, 2, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 62, null, 3, 1, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 63, null, 3, 9, "Кленовый лист", null, null, 31.100000000000001 },
                    { 64, null, 3, 27, "Американский Орел", null, null, 31.100000000000001 },
                    { 65, null, 3, 5, "Британия", null, null, 31.100000000000001 },
                    { 66, null, 3, 1, "Утконос", null, null, 31.100000000000001 },
                    { 67, null, 3, 5, "Королевский Герб", null, null, 31.100000000000001 },
                    { 68, null, 3, 5, "Белая лошадь Ганновера", null, null, 31.100000000000001 },
                    { 69, null, 3, 5, "Белый Грейхаунд Ричмонда", null, null, 31.100000000000001 },
                    { 70, null, 3, 5, "Звери королевы", null, null, 31.100000000000001 },
                    { 71, null, 3, 5, "Белый лев Мортимера", null, null, 31.100000000000001 },
                    { 72, null, 3, 5, "Королевский Единорог", null, null, 31.100000000000001 },
                    { 73, null, 3, 28, "Олимпийские игры", null, null, 15.550000000000001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinCatalogs_MintCountryId",
                table: "CoinCatalogs",
                column: "MintCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPrices_CityId",
                table: "CoinPrices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPrices_CoinFromCatalogId",
                table: "CoinPrices",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPrices_DealerId",
                table: "CoinPrices",
                column: "DealerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinsHistory_CoinCatalogs_CoinFromCatalogId",
                table: "CoinsHistory",
                column: "CoinFromCatalogId",
                principalTable: "CoinCatalogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DillerCoinMaps_CoinCatalogs_CoinFromCatalogId",
                table: "DillerCoinMaps",
                column: "CoinFromCatalogId",
                principalTable: "CoinCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinsHistory_CoinCatalogs_CoinFromCatalogId",
                table: "CoinsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_DillerCoinMaps_CoinCatalogs_CoinFromCatalogId",
                table: "DillerCoinMaps");

            migrationBuilder.DropTable(
                name: "CoinPrices");

            migrationBuilder.DropTable(
                name: "CoinCatalogs");

            migrationBuilder.DropTable(
                name: "MintCountries");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    EndMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MetalType = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Nomination = table.Column<string>(type: "text", nullable: true),
                    StartMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: true),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PriceSpecial = table.Column<double>(type: "double precision", nullable: false),
                    PriceToBuy = table.Column<double>(type: "double precision", nullable: false),
                    PriceToSell = table.Column<double>(type: "double precision", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coins_Catalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coins_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coins_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Австралия" },
                    { 2, "Австрия" },
                    { 3, "Армения" },
                    { 4, "Беларусь" },
                    { 5, "Великобритания" },
                    { 6, "Германия" },
                    { 7, "Казахстан" },
                    { 8, "Камерун" },
                    { 9, "Канада" },
                    { 10, "Китай" },
                    { 11, "Конго" },
                    { 12, "Кыргыстан" },
                    { 13, "Либерия" },
                    { 14, "Малави" },
                    { 15, "Мексика" },
                    { 16, "Монголия" },
                    { 17, "Науру" },
                    { 18, "Ниуэ" },
                    { 19, "Острова Мэн" },
                    { 20, "Острова Кука" },
                    { 21, "Палау" },
                    { 22, "Россия" },
                    { 23, "Руанда" },
                    { 24, "Сент-Китс И Невис" },
                    { 25, "Соломоновы Острова" },
                    { 26, "Сомали" },
                    { 27, "США" },
                    { 28, "СССР" },
                    { 29, "Тувалу" },
                    { 30, "Украина" },
                    { 31, "Фиджи" },
                    { 32, "Франция" },
                    { 33, "Швейцария" },
                    { 34, "ЮАР" },
                    { 35, "Южная Корея" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "1e4938e9-9c33-4ae3-b450-232ea12f7736");

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "CountryId", "EndMiningYear", "MetalType", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[,]
                {
                    { 1, 22, null, 1, "Георгий Победоносец", "200 рублей", null, 31.100000000000001 },
                    { 2, 27, null, 1, "Американский Буффало", null, null, 31.100000000000001 },
                    { 3, 2, null, 1, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 4, 1, null, 1, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 5, 9, null, 1, "Кленовый лист", null, null, 31.100000000000001 },
                    { 6, 27, null, 1, "Американский Орел", null, null, 31.100000000000001 },
                    { 7, 5, null, 1, "Британия", null, null, 31.100000000000001 },
                    { 8, 34, null, 1, "Крюгерранд", null, null, 31.100000000000001 },
                    { 9, 1, null, 1, "Австраллийский Лунар", null, null, 31.100000000000001 },
                    { 10, 10, null, 1, "Панда", null, null, 31.100000000000001 },
                    { 11, 22, null, 1, "Георгий Победоносец", null, null, 15.550000000000001 },
                    { 12, 27, null, 1, "Американский Буффало", null, null, 15.550000000000001 },
                    { 13, 2, null, 1, "Венский Филармоникер", null, null, 15.550000000000001 },
                    { 14, 1, null, 1, "Австраллийский Кенгуру", null, null, 15.550000000000001 },
                    { 15, 9, null, 1, "Кленовый лист", null, null, 15.550000000000001 },
                    { 16, 27, null, 1, "Американский Орел", null, null, 15.550000000000001 },
                    { 17, 5, null, 1, "Британия", null, null, 15.550000000000001 },
                    { 18, 34, null, 1, "Крюгерранд", null, null, 15.550000000000001 },
                    { 19, 1, null, 1, "Австраллийский Лунар", null, null, 15.550000000000001 },
                    { 20, 10, null, 1, "Панда", null, null, 15.550000000000001 },
                    { 21, 22, null, 1, "Георгий Победоносец", null, null, 7.7800000000000002 },
                    { 22, 27, null, 1, "Американский Буффало", null, null, 7.7800000000000002 },
                    { 23, 2, null, 1, "Венский Филармоникер", null, null, 7.7800000000000002 },
                    { 24, 1, null, 1, "Австраллийский Кенгуру", null, null, 7.7800000000000002 },
                    { 25, 9, null, 1, "Кленовый лист", null, null, 7.7800000000000002 },
                    { 26, 27, null, 1, "Американский Орел", null, null, 7.7800000000000002 },
                    { 27, 5, null, 1, "Британия", null, null, 7.7800000000000002 },
                    { 28, 34, null, 1, "Крюгерранд", null, null, 7.7800000000000002 },
                    { 29, 1, null, 1, "Австраллийский Лунар", null, null, 7.7800000000000002 },
                    { 30, 10, null, 1, "Панда", null, null, 7.7800000000000002 },
                    { 41, 22, null, 1, "Георгий Победоносец", null, null, 3.1099999999999999 },
                    { 42, 27, null, 1, "Американский Буффало", null, null, 3.1099999999999999 },
                    { 43, 2, null, 1, "Венский Филармоникер", null, null, 3.1099999999999999 },
                    { 44, 1, null, 1, "Австраллийский Кенгуру", null, null, 3.1099999999999999 },
                    { 45, 9, null, 1, "Кленовый лист", null, null, 3.1099999999999999 },
                    { 46, 27, null, 1, "Американский Орел", null, null, 3.1099999999999999 },
                    { 47, 5, null, 1, "Британия", null, null, 3.1099999999999999 },
                    { 48, 34, null, 1, "Крюгерранд", null, null, 3.1099999999999999 },
                    { 49, 1, null, 1, "Австраллийский Лунар", null, null, 3.1099999999999999 },
                    { 50, 10, null, 1, "Панда", null, null, 3.1099999999999999 },
                    { 51, 22, null, 2, "Георгий Победоносец", null, null, 31.100000000000001 },
                    { 52, 27, null, 2, "Американский Буффало", null, null, 31.100000000000001 },
                    { 53, 2, null, 2, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 54, 1, null, 2, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 55, 9, null, 2, "Кленовый лист", null, null, 31.100000000000001 },
                    { 56, 27, null, 2, "Американский Орел", null, null, 31.100000000000001 },
                    { 57, 5, null, 2, "Британия", null, null, 31.100000000000001 },
                    { 58, 34, null, 2, "Крюгерранд", null, null, 31.100000000000001 },
                    { 59, 1, null, 2, "Австраллийский Лунар", null, null, 31.100000000000001 },
                    { 60, 10, null, 2, "Панда", null, null, 31.109999999999999 },
                    { 61, 2, null, 3, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 62, 1, null, 3, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 63, 9, null, 3, "Кленовый лист", null, null, 31.100000000000001 },
                    { 64, 27, null, 3, "Американский Орел", null, null, 31.100000000000001 },
                    { 65, 5, null, 3, "Британия", null, null, 31.100000000000001 },
                    { 66, 1, null, 3, "Утконос", null, null, 31.100000000000001 },
                    { 67, 5, null, 3, "Королевский Герб", null, null, 31.100000000000001 },
                    { 68, 5, null, 3, "Белая лошадь Ганновера", null, null, 31.100000000000001 },
                    { 69, 5, null, 3, "Белый Грейхаунд Ричмонда", null, null, 31.100000000000001 },
                    { 70, 5, null, 3, "Звери королевы", null, null, 31.100000000000001 },
                    { 71, 5, null, 3, "Белый лев Мортимера", null, null, 31.100000000000001 },
                    { 72, 5, null, 3, "Королевский Единорог", null, null, 31.100000000000001 },
                    { 73, 28, null, 3, "Олимпийские игры", null, null, 15.550000000000001 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_CountryId",
                table: "Catalogs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CityId",
                table: "Coins",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CoinFromCatalogId",
                table: "Coins",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_DealerId",
                table: "Coins",
                column: "DealerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinsHistory_Catalogs_CoinFromCatalogId",
                table: "CoinsHistory",
                column: "CoinFromCatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DillerCoinMaps_Catalogs_CoinFromCatalogId",
                table: "DillerCoinMaps",
                column: "CoinFromCatalogId",
                principalTable: "Catalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
