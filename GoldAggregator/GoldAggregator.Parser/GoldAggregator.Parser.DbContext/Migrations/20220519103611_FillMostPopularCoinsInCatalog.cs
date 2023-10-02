using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class FillMostPopularCoinsInCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndMiningYear", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[] { null, "Георгий Победоносец", "200 рублей", null, 31.100000000000001 });

            migrationBuilder.UpdateData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CountryId", "EndMiningYear", "Name", "StartMiningYear", "Weight" },
                values: new object[] { 27, null, "Американский Буффало", null, 31.100000000000001 });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "CountryId", "EndMiningYear", "MetalType", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[,]
                {
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

            migrationBuilder.UpdateData(
                table: "DillerCoinMaps",
                keyColumn: "Id",
                keyValue: 3,
                column: "CoinFromCatalogId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DillerCoinMaps",
                keyColumn: "Id",
                keyValue: 4,
                column: "CoinFromCatalogId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "1e4938e9-9c33-4ae3-b450-232ea12f7736");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.UpdateData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndMiningYear", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[] { new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Георгий Победоносец СПМД", null, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.7800000000000002 });

            migrationBuilder.UpdateData(
                table: "Catalogs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CountryId", "EndMiningYear", "Name", "StartMiningYear", "Weight" },
                values: new object[] { 22, new DateTime(2012, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Георгий Победоносец СПМД", new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.7800000000000002 });

            migrationBuilder.UpdateData(
                table: "DillerCoinMaps",
                keyColumn: "Id",
                keyValue: 3,
                column: "CoinFromCatalogId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "DillerCoinMaps",
                keyColumn: "Id",
                keyValue: 4,
                column: "CoinFromCatalogId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "04b8a6e7-204f-48b9-96ce-2a294f4c7d9f");
        }
    }
}
