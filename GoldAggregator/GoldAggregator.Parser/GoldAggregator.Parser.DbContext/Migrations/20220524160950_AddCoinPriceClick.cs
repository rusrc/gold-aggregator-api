using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddCoinPriceClick : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinsHistory");

            migrationBuilder.CreateTable(
                name: "CoinPriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    PriceToBuy = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceToSell = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceSpecial = table.Column<decimal>(type: "numeric", nullable: false),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinPriceHistory_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinPriceHistory_CoinCatalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "CoinCatalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinPriceHistory_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinPriceClick",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ip = table.Column<string>(type: "text", nullable: true),
                    RedirectDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CoinHistoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinPriceClick", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinPriceClick_CoinPriceHistory_CoinHistoryId",
                        column: x => x.CoinHistoryId,
                        principalTable: "CoinPriceHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "8a12dbd8-1cda-4aad-abfa-b015c15b489a");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceClick_CoinHistoryId",
                table: "CoinPriceClick",
                column: "CoinHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceHistory_CityId",
                table: "CoinPriceHistory",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceHistory_CoinFromCatalogId",
                table: "CoinPriceHistory",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceHistory_DealerId",
                table: "CoinPriceHistory",
                column: "DealerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinPriceClick");

            migrationBuilder.DropTable(
                name: "CoinPriceHistory");

            migrationBuilder.CreateTable(
                name: "CoinsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PriceSpecial = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceToBuy = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceToSell = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinsHistory_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinsHistory_CoinCatalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "CoinCatalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinsHistory_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "eefb4d9d-e0a5-4c17-9eeb-7553288668bd");

            migrationBuilder.CreateIndex(
                name: "IX_CoinsHistory_CityId",
                table: "CoinsHistory",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinsHistory_CoinFromCatalogId",
                table: "CoinsHistory",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinsHistory_DealerId",
                table: "CoinsHistory",
                column: "DealerId");
        }
    }
}
