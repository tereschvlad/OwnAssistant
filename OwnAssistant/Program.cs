using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using OwnAssistantCommon.Models;
using Serilog;
using System.Globalization;
using OwnAssistant.Utils;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var config = builder.Configuration;

    builder.Services.AddDbContext<DataContext>(sql => sql.UseSqlServer(config.GetConnectionString("DefaultConnection")));

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();

    #region Customer services

    builder.Services.AddScoped<IDbRepository, DbRepository>();
    builder.Services.AddScoped<IAccountService, AccountService>();

    #endregion

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
    {
        option.LoginPath = "/Authorize/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        option.SlidingExpiration = true;
    });
    builder.Services.AddAuthorization();

    builder.Host.UseSerilog((logger, dispose, prov) => prov
        .ReadFrom.Configuration(logger.Configuration)
        .ReadFrom.Services(dispose)
        .Enrich.FromLogContext()
    );

    var app = builder.Build();

    app.UseCookiePolicy();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();
    app.MapDefaultControllerRoute();

    //Add test data into DB
    DbDataFilling.FillData(app);

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed");
}
finally
{
    Log.CloseAndFlush();
}


