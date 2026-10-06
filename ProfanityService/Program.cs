using ProfanityService.Repositories;
using ProfanityService.Data;
using ProfanityService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<ProfanityRepository>();
builder.Services.AddScoped<ProfanityFilterService>();


builder.Services.AddDbContext<ProfanityContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("ProfanityDatabase")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();