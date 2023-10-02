using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoldAggregator.Parser.DbContext.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Alias = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProviderName = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Nomination = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    MetalType = table.Column<int>(type: "integer", nullable: false),
                    StartMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndMiningYear = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogs_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProviderName = table.Column<string>(type: "text", nullable: true),
                    BaseUrl = table.Column<string>(type: "text", nullable: true),
                    DealerType = table.Column<int>(type: "integer", nullable: false),
                    UrlInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dealers_UrlInfos_UrlInfoId",
                        column: x => x.UrlInfoId,
                        principalTable: "UrlInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    ExternalId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    UrlInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urls_UrlInfos_UrlInfoId",
                        column: x => x.UrlInfoId,
                        principalTable: "UrlInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    PriceToBuy = table.Column<double>(type: "double precision", nullable: false),
                    PriceToSell = table.Column<double>(type: "double precision", nullable: false),
                    PriceSpecial = table.Column<double>(type: "double precision", nullable: false),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: true),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coins_Catalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coins_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Coins_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoinsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    PriceToBuy = table.Column<double>(type: "double precision", nullable: false),
                    PriceToSell = table.Column<double>(type: "double precision", nullable: false),
                    PriceSpecial = table.Column<double>(type: "double precision", nullable: false),
                    ParseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinsHistory_Catalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinsHistory_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoinsHistory_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DillerCoinMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchedName = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    DealerId = table.Column<int>(type: "integer", nullable: false),
                    CoinFromCatalogId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DillerCoinMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DillerCoinMaps_Catalogs_CoinFromCatalogId",
                        column: x => x.CoinFromCatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DillerCoinMaps_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Alias", "Name" },
                values: new object[,]
                {
                    { 1, "moskva", "Москва" },
                    { 2, "sankt-peterburg", "Санкт-Петербург" },
                    { 3, "nizhnij-novgorod", "Нижний Новгород" },
                    { 4, "kazan", "Казань" },
                    { 5, "surgut", "Сургут" },
                    { 6, "sevastopol", "Севастополь" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Австралия" },
                    { 2, "Австрия" },
                    { 3, "Армения" },
                    { 4, "Беларусь" },
                    { 5, "Великобритания" },
                    { 6, "Германия" },
                    { 7, "Казахстан" },
                    { 8, "Камерун" },
                    { 9, "Канада" },
                    { 10, "Китай" },
                    { 11, "Конго" },
                    { 12, "Кыргыстан" },
                    { 13, "Либерия" },
                    { 14, "Малави" },
                    { 15, "Мексика" },
                    { 16, "Монголия" },
                    { 17, "Науру" },
                    { 18, "Ниуэ" },
                    { 19, "Острова Мэн" },
                    { 20, "Острова Кука" },
                    { 21, "Палау" },
                    { 22, "Россия" },
                    { 23, "Руанда" },
                    { 24, "Сент-Китс И Невис" },
                    { 25, "Соломоновы Острова" },
                    { 26, "Сомали" },
                    { 27, "США" },
                    { 28, "СССР" },
                    { 29, "Тувалу" },
                    { 30, "Украина" },
                    { 31, "Фиджи" },
                    { 32, "Франция" },
                    { 33, "Швейцария" },
                    { 34, "ЮАР" },
                    { 35, "Южная Корея" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d0c691d4-98dc-4ee6-b9db-633fcc9c238e", "04b8a6e7-204f-48b9-96ce-2a294f4c7d9f", "Admin", null });

            migrationBuilder.InsertData(
                table: "UrlInfos",
                columns: new[] { "Id", "ErrorMessage", "ProviderName", "StackTrace" },
                values: new object[,]
                {
                    { 1, null, "ZolotoMdRuProvider", null },
                    { 2, null, "MonetaInvestProvider", null },
                    { 3, null, "ZolotoPiterRuProvider", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "25723030-b8e5-4b2a-8c06-5cf170e3443b", 0, "9ac88e11-e377-4a49-bef9-ed7767ff6f9d", "admin@mail.ru", false, false, null, "ADMIN@MAIL.RU", "ADMIN@MAIL.RU", "AQAAAAEAACcQAAAAEA7vpzNBUIMLvB4bdfb8xX5IIsMZ86GfG1In4YX3q8BYyZoFQYSGuVOVWB3XfCdZOA==", null, false, "6FPVSOIY6BCPOQH4BZUCNYIKQ5WB4VRM", false, "admin@mail.ru" });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "CountryId", "EndMiningYear", "MetalType", "Name", "Nomination", "StartMiningYear", "Weight" },
                values: new object[,]
                {
                    { 1, 22, new DateTime(2022, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Георгий Победоносец СПМД", null, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.7800000000000002 },
                    { 2, 22, new DateTime(2012, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Георгий Победоносец СПМД", null, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7.7800000000000002 },
                    { 3, 2, null, 1, "Венский Филармоникер", null, null, 31.100000000000001 },
                    { 4, 1, null, 1, "Австраллийский Кенгуру", null, null, 31.100000000000001 },
                    { 5, 9, null, 1, "Кленовый лист", null, null, 31.100000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "BaseUrl", "DealerType", "Name", "ProviderName", "UrlInfoId" },
                values: new object[,]
                {
                    { 1, "https://zoloto-md.ru", 4, "ZolotoMD", "ZolotoMdRuProvider", 1 },
                    { 2, "https://monetainvest.ru/", 4, "MonetaInvest", "MonetaInvestProvider", 2 },
                    { 3, "http://zoloto-piter.ru/", 4, "ZolotoPiterRu", "ZolotoPiterRuProvider", 3 }
                });

            migrationBuilder.InsertData(
                table: "Urls",
                columns: new[] { "Id", "CreateDate", "ErrorMessage", "ExternalId", "ModifiedDate", "StackTrace", "Status", "UrlInfoId", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "https://zoloto-md.ru" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, "https://monetainvest.ru" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, "http://zoloto-piter.ru" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d0c691d4-98dc-4ee6-b9db-633fcc9c238e", "25723030-b8e5-4b2a-8c06-5cf170e3443b" });

            migrationBuilder.InsertData(
                table: "DillerCoinMaps",
                columns: new[] { "Id", "CoinFromCatalogId", "DealerId", "MatchedName", "Url" },
                values: new object[,]
                {
                    { 1, 3, 1, "Золотая инвестиционная монета Австрии Венский Филармоникер, 31,1 гр чистого золота (проба 0,9999)", "https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-investiczionnaya-moneta-avstrijskij-filarmoniker" },
                    { 2, 5, 1, "Золотая монета Канады \"Кленовый Лист\", 31.1 г чистого золота (проба 0.9999)", "https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-moneta-kanadyi-klenovyij-list,-31.1-g-chistogo-zolota-proba-0.9999" },
                    { 3, 2, 1, "Золотая инвестиционная монета Георгий ПОБЕДОНОСЕЦ СПМД 2006 - 2012 г.в., вес чистого золота - 7.78 г (проба 0,999)", "https://zoloto-md.ru/bullion-coins/i-rossiya-i-sssr/zolotaya-investiczionnaya-moneta-georgij-pobedonosecz-spmd" },
                    { 4, 2, 1, "Золотая инвестиционная монета Георгий Победоносец СПМД 2018-2022 г.в., 7.78 г чистого золота (проба 0,999)", "https://zoloto-md.ru/bullion-coins/i-rossiya-i-sssr/zolotaya-investiczionnaya-moneta-georgij-pobedonosecz-mmd,-7,78-g-chistogo-zolota-proba-0,999" },
                    { 5, 4, 1, "Золотая инвестиционная монета Австралии - Кенгуру 2022 г.в., 31.1 г чистого золота (проба 0,9999)", "https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-investiczionnaya-moneta-avstralii-kenguru-2022-g.v.,-31.1-g-chistogo-zolota-proba-0,9999" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalogs_CountryId",
                table: "Catalogs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CityId",
                table: "Coins",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_CoinFromCatalogId",
                table: "Coins",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Coins_DealerId",
                table: "Coins",
                column: "DealerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UrlInfoId",
                table: "Dealers",
                column: "UrlInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DillerCoinMaps_CoinFromCatalogId",
                table: "DillerCoinMaps",
                column: "CoinFromCatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_DillerCoinMaps_DealerId",
                table: "DillerCoinMaps",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlInfos_ProviderName",
                table: "UrlInfos",
                column: "ProviderName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urls_UrlInfoId",
                table: "Urls",
                column: "UrlInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Urls_Value",
                table: "Urls",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "CoinsHistory");

            migrationBuilder.DropTable(
                name: "DillerCoinMaps");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Catalogs");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "UrlInfos");
        }
    }
}
