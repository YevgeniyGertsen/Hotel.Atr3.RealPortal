﻿using Hotel.Atr3.RealPortal.AppFilter;
using Hotel.Atr3.RealPortal.AppMidleware;
using Hotel.Atr3.RealPortal.Models;
using Hotel.Atr3.RealPortal.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => 
{
    options.Filters.Add<TimeElapsedFilter>();    
    options.Filters.Add<CatchError>();    
})
.AddViewLocalization();


#region DI
builder.Services.AddTransient<IMessage, SmsSender>();
//builder.Services.AddTransient<IMessage, EmailSender>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();

builder.Services.AddScoped<TokenService>();
#endregion

builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


#region Localizer

//через файлы Resources
//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

//builder.Services.Configure<RequestLocalizationOptions>(options => 
//{
//    var supportedCulture = new[]
//    {
//        new CultureInfo("kk-KZ"),
//        new CultureInfo("ru-RU"),
//        new CultureInfo("en-US")
//    };

//    options.DefaultRequestCulture = new RequestCulture(culture: "kk-KZ", uiCulture: "kk-KZ");
//    options.SupportedCultures = supportedCulture;
//    options.SupportedUICultures = supportedCulture;
//});


//через базу данных
var serviceProvider = builder.Services.BuildServiceProvider();
var languageService = serviceProvider.GetService<ILanguageService>();

var languages = languageService.GetLanguages();
var culture = languages.Select(x => new CultureInfo(x.Culture)).ToList();
var defaultCulture = languages.FirstOrDefault(f => f.IsDefaultCulture);

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = 
    new RequestCulture(culture: defaultCulture.Culture,
                       uiCulture: defaultCulture.Culture);

    options.SupportedCultures = culture;
    options.SupportedUICultures = culture;
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



builder.WebHost.UseUrls("http://localhost:5260");

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

app.Use(async (context, next) =>
{

    await next.Invoke();
});

//app.UseMiddleware<UseWorkTime>();
//app.UseWorkTime();


#region routing templates
////rooms → Показать все номера
//app.MapControllerRoute(
//	name: "rooms",
//	pattern: "rooms",
//	defaults: new { controller = "Room", action = "Index" });

////rooms?category=luxury → Показать номера категории "люкс"
//app.MapControllerRoute(
//	name: "GetRoomLuxury",
//	pattern: "room/category/{category=luxury}",
//	defaults: new { controller = "Room", action = "RoomList" });

////rooms?category=standard&capacity=2 → Показать стандартные номера на 2 человек
//app.MapControllerRoute(
//	name: "GetRoomLuxury",
//	pattern: "room/category/{category}/{capacity:int}",
//	defaults: new { controller = "Room", action = "RoomList" });

////rooms/101 → Показать номер с ID 101
//app.MapControllerRoute(
//	name: "RoomDetails",
//	pattern: "room/{roomId:int}",
//	defaults: new { controller = "Room", action = "RoomDetails" });

//app.MapControllerRoute(
//	name: "contactUs",
//	pattern: "contact-us",
//    defaults: new { controller = "Home", action="Contact"} );

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();
