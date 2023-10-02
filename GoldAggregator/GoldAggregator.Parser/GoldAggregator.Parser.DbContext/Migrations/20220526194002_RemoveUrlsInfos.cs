using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class RemoveUrlsInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_UrlInfos_UrlInfoId",
                table: "Dealers");

            migrationBuilder.DropForeignKey(
                name: "FK_Urls_UrlInfos_UrlInfoId",
                table: "Urls");

            migrationBuilder.DropTable(
                name: "UrlInfos");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_UrlInfoId",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "UrlInfoId",
                table: "Dealers");

            migrationBuilder.RenameColumn(
                name: "UrlInfoId",
                table: "Urls",
                newName: "DealerId");

            migrationBuilder.RenameIndex(
                name: "IX_Urls_UrlInfoId",
                table: "Urls",
                newName: "IX_Urls_DealerId");

            migrationBuilder.AddColumn<string>(
                name: "PriceSpecialDetails",
                table: "CoinPriceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSpecialPerGram",
                table: "CoinPriceHistory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToBuyPerGram",
                table: "CoinPriceHistory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToSellPerGram",
                table: "CoinPriceHistory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "75032e4c-5ce9-47da-87d2-962ca1b69dfb");

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_Dealers_DealerId",
                table: "Urls",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urls_Dealers_DealerId",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "PriceSpecialDetails",
                table: "CoinPriceHistory");

            migrationBuilder.DropColumn(
                name: "PriceSpecialPerGram",
                table: "CoinPriceHistory");

            migrationBuilder.DropColumn(
                name: "PriceToBuyPerGram",
                table: "CoinPriceHistory");

            migrationBuilder.DropColumn(
                name: "PriceToSellPerGram",
                table: "CoinPriceHistory");

            migrationBuilder.RenameColumn(
                name: "DealerId",
                table: "Urls",
                newName: "UrlInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Urls_DealerId",
                table: "Urls",
                newName: "IX_Urls_UrlInfoId");

            migrationBuilder.AddColumn<int>(
                name: "UrlInfoId",
                table: "Dealers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UrlInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    ProviderName = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlInfos", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "90702784-4b11-4b0f-b905-ae533730f3cd");

            migrationBuilder.InsertData(
                table: "UrlInfos",
                columns: new[] { "Id", "ErrorMessage", "ProviderName", "StackTrace" },
                values: new object[,]
                {
                    { 1, null, "ZolotoMdRuProvider", null },
                    { 2, null, "MonetaInvestProvider", null },
                    { 3, null, "ZolotoPiterRuProvider", null },
                    { 4, null, "Удалить", null },
                    { 5, null, "Удалить2", null },
                    { 6, null, "Удалить3", null },
                    { 7, null, "Удалить4", null },
                    { 8, null, "Удалить5", null },
                    { 9, null, "Удалить6", null },
                    { 10, null, "Удалить7", null }
                });

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1,
                column: "UrlInfoId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 2,
                column: "UrlInfoId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 3,
                column: "UrlInfoId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 4,
                column: "UrlInfoId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 5,
                column: "UrlInfoId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 6,
                column: "UrlInfoId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 7,
                column: "UrlInfoId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 8,
                column: "UrlInfoId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 9,
                column: "UrlInfoId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 10,
                column: "UrlInfoId",
                value: 10);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UrlInfoId",
                table: "Dealers",
                column: "UrlInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlInfos_ProviderName",
                table: "UrlInfos",
                column: "ProviderName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_UrlInfos_UrlInfoId",
                table: "Dealers",
                column: "UrlInfoId",
                principalTable: "UrlInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Urls_UrlInfos_UrlInfoId",
                table: "Urls",
                column: "UrlInfoId",
                principalTable: "UrlInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
