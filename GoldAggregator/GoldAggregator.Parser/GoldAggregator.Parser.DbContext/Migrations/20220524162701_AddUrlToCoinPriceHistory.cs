using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddUrlToCoinPriceHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "CoinPriceHistory",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "57bff50c-14fb-448d-a739-529605a71058");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "CoinPriceHistory");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "8a12dbd8-1cda-4aad-abfa-b015c15b489a");
        }
    }
}
