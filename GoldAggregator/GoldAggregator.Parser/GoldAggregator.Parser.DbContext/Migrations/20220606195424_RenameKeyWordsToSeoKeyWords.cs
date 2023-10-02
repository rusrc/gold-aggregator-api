using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class RenameKeyWordsToSeoKeyWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KeyWords",
                table: "Dealers",
                newName: "SeoKeyWords");

            migrationBuilder.RenameColumn(
                name: "KeyWords",
                table: "CoinCatalogs",
                newName: "SeoKeyWords");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "c96faa9e-596a-45ec-8881-39af024af2be");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SeoKeyWords",
                table: "Dealers",
                newName: "KeyWords");

            migrationBuilder.RenameColumn(
                name: "SeoKeyWords",
                table: "CoinCatalogs",
                newName: "KeyWords");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "e68dfff2-9261-4bba-a94c-85ea50ee3fd1");
        }
    }
}
