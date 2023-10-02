using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddIsClickBySellPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClickBySellPrice",
                table: "CoinPriceClick",
                type: "boolean",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "4e67dff4-116e-40f1-9685-b02694684818");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClickBySellPrice",
                table: "CoinPriceClick");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "7b905204-c1e5-4d4d-a1ed-dc578c29df0a");
        }
    }
}
