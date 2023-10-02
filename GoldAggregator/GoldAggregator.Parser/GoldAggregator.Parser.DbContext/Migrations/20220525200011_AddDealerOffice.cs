using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddDealerOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinPriceHistory_Cities_CityId",
                table: "CoinPriceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_CoinPrices_Cities_CityId",
                table: "CoinPrices");

            migrationBuilder.DropIndex(
                name: "IX_CoinPrices_CityId",
                table: "CoinPrices");

            migrationBuilder.DropIndex(
                name: "IX_CoinPriceHistory_CityId",
                table: "CoinPriceHistory");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CoinPrices");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CoinPriceHistory");

            migrationBuilder.AddColumn<bool>(
                name: "HasDelivery",
                table: "Dealers",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "CoinPriceClick",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DealerOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    DealerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerOffices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerOffices_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DealerOffices_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DealerOffices",
                columns: new[] { "Id", "CityId", "DealerId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 6, 1 },
                    { 6, 2, 2 },
                    { 7, 2, 3 },
                    { 8, 1, 4 },
                    { 9, 1, 5 },
                    { 10, 1, 6 },
                    { 11, 2, 7 },
                    { 12, 2, 8 },
                    { 13, 2, 9 },
                    { 14, 1, 10 }
                });

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HasDelivery", "Name" },
                values: new object[] { false, "Золотой монетный дом" });

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 2,
                column: "HasDelivery",
                value: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "adf6b898-c93a-4cb2-86ca-e423fcedd7b1");

            migrationBuilder.CreateIndex(
                name: "IX_DealerOffices_CityId",
                table: "DealerOffices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerOffices_DealerId",
                table: "DealerOffices",
                column: "DealerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DealerOffices");

            migrationBuilder.DropColumn(
                name: "HasDelivery",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "CoinPriceClick");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "CoinPrices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "CoinPriceHistory",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Dealers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Золотой монетный дома");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "f58f2524-d302-40a2-bd64-42a07b2bbf78");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPrices_CityId",
                table: "CoinPrices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinPriceHistory_CityId",
                table: "CoinPriceHistory",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinPriceHistory_Cities_CityId",
                table: "CoinPriceHistory",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinPrices_Cities_CityId",
                table: "CoinPrices",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
