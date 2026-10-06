using ArticleService.Repositories;
using ArticleService.Services;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using ArticleService.Data;
using ArticleService.Models;
using Monitoring;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

TracingService.Configure();
LoggingService.Configure(builder);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped <DbRouter>();

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("ArticleCache")!));
builder.Services.AddSingleton<ArticleCache>();
builder.Services.AddHostedService<ArticleCacheRefreshService>();

builder.Services.AddHostedService<ArticleQueueSubscriber>();
builder.Services.AddEasyNetQ(builder.Configuration.GetConnectionString("ArticleQueueClient"));
builder.Services.AddDbContext<ArticleContext>((services, options) =>
{
    DbRouter router = services.GetRequiredService<DbRouter>();
    options.UseNpgsql(
        router.GetConnectionString(Continent.Global));
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

var app = builder.Build();
app.Services.GetRequiredService<ArticleCache>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

await PrepDB.MigrateAndSeedData(app.Services);
app.MapMetrics();
app.Run();
