using DraftService.Repositories;
using DraftService.Service;
using DraftService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();
app.MapControllers();

app.Run();