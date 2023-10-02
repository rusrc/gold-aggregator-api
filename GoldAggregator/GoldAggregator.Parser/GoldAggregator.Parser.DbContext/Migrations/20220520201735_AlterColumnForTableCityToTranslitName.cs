using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AlterColumnForTableCityToTranslitName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alias",
                table: "Cities",
                newName: "TranslitName");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "ccc48605-7dbf-4c94-97d8-bf439312afd5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TranslitName",
                table: "Cities",
                newName: "Alias");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "829e0cb0-7b54-487f-a45c-f73b71a4a279");
        }
    }
}
