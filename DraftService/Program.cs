using DraftService.Repositories;
using DraftService.Services;
using DraftService.Data;
using Microsoft.EntityFrameworkCore;
using Monitoring;

var builder = WebApplication.CreateBuilder(args);

TracingService.Configure();
LoggingService.Configure(builder);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<DraftRepository>();
builder.Services.AddScoped<DraftManager>();
builder.Services.AddDbContext<DraftContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DraftDatabase")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

await PrepDB.MigrateAndSeedData(app.Services);
app.Run();