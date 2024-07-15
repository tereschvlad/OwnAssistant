using Microsoft.EntityFrameworkCore;
using OwnAssistantCommon.Interfaces;
using OwnAssistantCommon.Models;
using OwnAssistantWorker;
using OwnAssistantWorker.Models;
using OwnAssistantWorker.Services;
using Serilog;
using Telegram.Bot.Polling;


var builder = Host.CreateApplicationBuilder(args);

try
{
    var config = builder.Configuration;

    builder.Services.AddDbContext<DataContext>(sql => sql.UseSqlServer(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient, ServiceLifetime.Transient);

    builder.Services.AddTransient<IDbRepository, DbRepository>();
    builder.Services.AddTransient<IUpdateHandler, ChatMessageHandle>();

    builder.Services.Configure<TelegramBotConfiguration>(config.GetSection("TelegramBotConfig"));

    builder.Services.AddHostedService<ManagerTelegramMessageWorker>();
    builder.Services.AddHostedService<TaskManagerSenderWorker>();

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

