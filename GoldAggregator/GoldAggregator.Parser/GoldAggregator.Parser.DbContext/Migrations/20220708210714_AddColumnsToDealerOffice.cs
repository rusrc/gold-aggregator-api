using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class AddColumnsToDealerOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "DealerOffices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DealerOffices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "DealerOffices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "DealerOffices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WorkTime",
                table: "DealerOffices",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "b48dde05-c046-448f-9daa-631d97eef557");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "DealerOffices");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "DealerOffices");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "DealerOffices");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "DealerOffices");

            migrationBuilder.DropColumn(
                name: "WorkTime",
                table: "DealerOffices");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                column: "ConcurrencyStamp",
                value: "4e67dff4-116e-40f1-9685-b02694684818");
        }
    }
}
