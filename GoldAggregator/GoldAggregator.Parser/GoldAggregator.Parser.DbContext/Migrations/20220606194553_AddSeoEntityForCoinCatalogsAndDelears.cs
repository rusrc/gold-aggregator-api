using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddSeoEntityForCoinCatalogsAndDelears : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "Dealers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Dealers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Dealers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoUrl",
                table: "Dealers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "CoinCatalogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "CoinCatalogs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "CoinCatalogs",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "e68dfff2-9261-4bba-a94c-85ea50ee3fd1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "SeoUrl",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "CoinCatalogs");

            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "CoinCatalogs");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "CoinCatalogs");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "eeefed0a-9df1-4fcd-8075-ece7948fcecd");
        }
    }
}
