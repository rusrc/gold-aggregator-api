using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class RenameTableFieldToMatchedTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchedName",
                table: "DillerCoinMaps",
                newName: "MatchedTitle");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "b105e472-8c55-47bb-96fa-46361ea6edba");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchedTitle",
                table: "DillerCoinMaps",
                newName: "MatchedName");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "94c02bf6-2cee-4e3f-b148-a5d061f7b715");
        }
    }
}
