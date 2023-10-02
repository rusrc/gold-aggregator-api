using GoldAggregator.Parser.DbContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// TODO SetSwitch https://www.npgsql.org/doc/types/datetime.html
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (LocalConnection, DefaultConnection)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ParserDbContext>(options
    => options.UseNpgsql(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ParserDbContext>();

builder.Services.AddControllersWithViews();
// https://docs.microsoft.com/ru-ru/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-6.0
// builder.Services.AddControllersWithViews(option => option.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider()));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy",
        policy =>
        {
            policy.RequireRole("Admin", "Manager");
        });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services
    .AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/robots.txt"))
    {
        var robotsTxtPath = Path.Combine(builder.Environment.ContentRootPath, "robots.txt");
        string output = "User-agent: *  \nDisallow: /";
        if (File.Exists(robotsTxtPath))
        {
            output = await File.ReadAllTextAsync(robotsTxtPath);
        }
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(output);
    }
    else await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


var supportedCultures = new[] { "en-US", "ru" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.Run();
