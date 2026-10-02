using CommentService.Repositories;
using CommentService.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CommentService.Services;
using Resilience;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHttpClient<ProfanityClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration.GetConnectionString("ProfanityClient")!);
})
.AddResilienceHandler("profanity", HttpCircuitBreaker.Configure);

builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<CommentManager>();
builder.Services.AddDbContext<CommentContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("CommentDatabase")
    ));

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration.GetConnectionString("CommentCache") + ",abortConnect=false"));
builder.Services.AddSingleton<CommentCache>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

await PrepDB.MigrateAndSeedData(app.Services);
app.Run();