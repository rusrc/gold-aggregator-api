using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class SeedDealers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dealers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Монетный дилер в Москве, Санкт-Петребурге, Нижнем Новгороде, Севастополе, Казани. Покупка и продажа золотых, серебрянных, платиновых монет", "Золотой монетный дома" });

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Монетный дилер в Санкт-петребурге. Покупка и продажа золотых, серебрянных, платиновых монет", "Монета" });

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет", "ООО «Инвестиции в Золото»" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "f58f2524-d302-40a2-bd64-42a07b2bbf78");

            migrationBuilder.InsertData(
                table: "UrlInfos",
                columns: new[] { "Id", "ErrorMessage", "ProviderName", "StackTrace" },
                values: new object[,]
                {
                    { 4, null, "Удалить", null },
                    { 5, null, "Удалить2", null },
                    { 6, null, "Удалить3", null },
                    { 7, null, "Удалить4", null },
                    { 8, null, "Удалить5", null },
                    { 9, null, "Удалить6", null },
                    { 10, null, "Удалить7", null }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "BaseUrl", "DealerType", "Description", "Name", "ProviderName", "UrlInfoId" },
                values: new object[,]
                {
                    { 4, "http://a-fin.net/", 4, "Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет", "МонетаИнвест", "AFinProvider", 4 },
                    { 5, "https://golddep.ru/", 4, "Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет", "Золотой Департамент", "GoldDepProvider", 5 },
                    { 6, "https://9999d.ru/", 4, "Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет", "Золото Державы", "GoldDergavaProvider", 6 },
                    { 7, "http://petroinvest.ru/", 4, "Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет", "Петроинвест", "PetroinvestProvider", 7 },
                    { 8, "https://neva-gold.ru/", 4, "Золотые слитки в Санкт-Петербурге.", "Neva Gold", "NevaGoldProvider", 8 },
                    { 9, "http://ifk-pik.ru/", 4, "Монетный дилер в Санкт-Петербурге. Покупка и продажа золотых, серебрянных, платиновых монет", "Инвестиционно-Финансовая Компания \"Пик\"", "IFK-PikProvider", 9 },
                    { 10, "https://www.numizmatik.ru/", 4, "Монетный дилер в Москве. Покупка и продажа золотых, серебрянных, платиновых монет", "Клуб Нумизмат", "NumizmatikProvider", 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UrlInfos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dealers");

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "ZolotoMD");

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "MonetaInvest");

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "ZolotoPiterRu");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "57bff50c-14fb-448d-a739-529605a71058");
        }
    }
}
