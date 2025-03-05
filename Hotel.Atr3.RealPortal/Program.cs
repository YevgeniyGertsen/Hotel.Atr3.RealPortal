using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddViewLocalization();

#region Localizer

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options => 
{
    var supportedCulture = new[]
    {
        new CultureInfo("kk-KZ"),
        new CultureInfo("ru-RU"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "kk-KZ", uiCulture: "kk-KZ");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
});

#endregion

#region Auth
builder.Services.AddDbContext<AppIdentityDbContext>
    (options => options.UseSqlServer(
        builder.Configuration["ConnectionStrings:DefaultConnection"]));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region DI
builder.Services.AddTransient<IMessage, SmsSender>();
//builder.Services.AddTransient<IMessage, EmailSender>();
#endregion

#region Logging
//1 вариант логирования
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

//2 вариант логирования
var connection = builder.Configuration.GetConnectionString("DefaultConnection");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/hotelAtrLogs_.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: connection,
        sinkOptions: new MSSqlServerSinkOptions()
        {
            TableName = "Log",
            AutoCreateSqlTable = true
        })
    .WriteTo.Seq("http://localhost:5341/")
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Host.UseSerilog();
#endregion





var app = builder.Build();

#region Localizer
var supportedCulture = new[] { "kk-KZ", "ru-RU", "en-US" };
var localizerOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("kk-KZ")
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

app.UseRequestLocalization(localizerOptions);
#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
