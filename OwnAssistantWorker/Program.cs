using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using OwnAssistantWorker;
using OwnAssistantWorker.Models;
using Serilog;
using Telegram.Bot.Polling;


var builder = Host.CreateApplicationBuilder(args);

try
{
    var config = builder.Configuration;

    builder.Services.AddDbContext<DataContext>(sql => sql.UseSqlServer(config.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IDbRepository, DbRepository>();
    builder.Services.AddTransient<IUpdateHandler, UpdateHandler>();

    builder.Services.Configure<TelegramBotConfiguration>(config.GetSection("TelegramBotConfig"));

    builder.Services.AddHostedService<Worker>();

    builder.Services.AddHttpClient("telegram_bot_connection");

    var host = builder.Build();
    host.Run();
}
catch(Exception ex)
{
    Log.Fatal(ex, "Application crashed");
}
finally
{
    Log.CloseAndFlush();
}

