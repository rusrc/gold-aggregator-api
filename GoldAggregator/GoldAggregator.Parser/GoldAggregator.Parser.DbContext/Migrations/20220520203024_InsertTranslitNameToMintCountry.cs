using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class InsertTranslitNameToMintCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TranslitName",
                table: "MintCountries",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 1,
                column: "TranslitName",
                value: "avstraliya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 2,
                column: "TranslitName",
                value: "avstriya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 3,
                column: "TranslitName",
                value: "armeniya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 4,
                column: "TranslitName",
                value: "belarus");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 5,
                column: "TranslitName",
                value: "velikobritaniya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 6,
                column: "TranslitName",
                value: "germaniya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 7,
                column: "TranslitName",
                value: "kazaxstan");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 8,
                column: "TranslitName",
                value: "kamerun");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 9,
                column: "TranslitName",
                value: "kanada");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 10,
                column: "TranslitName",
                value: "kitaj");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 11,
                column: "TranslitName",
                value: "kongo");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 12,
                column: "TranslitName",
                value: "kyrgystan");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 13,
                column: "TranslitName",
                value: "liberiya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 14,
                column: "TranslitName",
                value: "malavi");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 15,
                column: "TranslitName",
                value: "meksika");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 16,
                column: "TranslitName",
                value: "mongoliya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 17,
                column: "TranslitName",
                value: "nauru");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 18,
                column: "TranslitName",
                value: "niue");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 19,
                column: "TranslitName",
                value: "ostrova-men");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 20,
                column: "TranslitName",
                value: "ostrova-kuka");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 21,
                column: "TranslitName",
                value: "palau");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 22,
                column: "TranslitName",
                value: "rossiya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 23,
                column: "TranslitName",
                value: "ruanda");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 24,
                column: "TranslitName",
                value: "sent-kits-i-nevis");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 25,
                column: "TranslitName",
                value: "solomonovy-ostrova");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 26,
                column: "TranslitName",
                value: "somali");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 27,
                column: "TranslitName",
                value: "ssha");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 28,
                column: "TranslitName",
                value: "sssr");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 29,
                column: "TranslitName",
                value: "tuvalu");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 30,
                column: "TranslitName",
                value: "ukraina");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 31,
                column: "TranslitName",
                value: "fidzhi");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 32,
                column: "TranslitName",
                value: "franciya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 33,
                column: "TranslitName",
                value: "shvejcariya");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 34,
                column: "TranslitName",
                value: "yuar");

            migrationBuilder.UpdateData(
                table: "MintCountries",
                keyColumn: "Id",
                keyValue: 35,
                column: "TranslitName",
                value: "yuzhnaya-koreya");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "e408601a-8c8b-4fff-911e-ffcd45374e66");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TranslitName",
                table: "MintCountries");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "ccc48605-7dbf-4c94-97d8-bf439312afd5");
        }
    }
}
