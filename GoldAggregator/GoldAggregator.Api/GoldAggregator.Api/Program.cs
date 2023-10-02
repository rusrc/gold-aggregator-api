using GoldAggregator.Api.ConfigurationExtensions;
using GoldAggregator.Api.Middlewares;
using GoldAggregator.Parser.Services;
using GoldAggregator.Parser.Services.Abstractions;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.HttpOverrides;

using System.Reflection;
using System.Text.Json.Serialization;

using static GoldAggregator.Api.ConfigurationExtensions.AddCustomParserDbContextExtension;


var builder = WebApplication.CreateBuilder(args);

// Read more about logging https://betterstack.com/community/guides/logging/how-to-start-logging-with-net/
builder.Services.AddLogging(c => c
    .AddDebug()
    // .AddConsole()
    .SetMinimumLevel(LogLevel.Warning)
);

// TODO SetSwitch https://www.npgsql.org/doc/types/datetime.html
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services.
builder.Services.AddParserDbContext(ConnectionTypeEnum.Default, builder.Configuration);

// TODO Добавил Identity, но не проверял раббот
builder.Services.AddIdentityForParserDbContext();
builder.Services
    .AddControllers()
    .AddJsonOptions(configure =>
    {
        // configure.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
        configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(Program));

// Read more https://docs.microsoft.com/ru-ru/aspnet/core/performance/caching/memory?view=aspnetcore-6.0
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

// Custom services

var useCache = builder.Configuration.GetValue<bool>("MemaryCaching:Use");
builder.Services.AddCustomRepositories(useCache);
builder.Services.AddCustomServices(useCache);
builder.Services.AddTransient<GrecaptchaService>();
builder.Services.AddTransient<TelegramService>();
builder.Services.AddTransient<IDeclineService, RussianDeclineService>();
builder.Services.AddTransient<JsonLdService>();

// ------------------------------
var app = builder.Build();

// https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-6.0
app.MapHealthChecks("/healthz");

// https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/http-logging/?view=aspnetcore-6.0
// app.UseHttpLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// No need. Because Nginx use reverse proxy
// app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.ConfigureExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
