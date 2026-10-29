using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using SubscriberService.Data;
using SubscriberService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<SubscriberContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("SubscriberDatabase")));
builder.Services.AddEasyNetQ(builder.Configuration.GetConnectionString("SubscriberQueue"));
builder.Services.AddSingleton<ReleaseToggle>();
builder.Services.AddScoped<SubscriberQueuePublisher>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<SubscriberContext>();
    await database.Database.EnsureCreatedAsync();
    if (!await database.FeatureStates.AnyAsync(state => state.Id == 1))
    {
        database.FeatureStates.Add(new SubscriberFeatureState { Id = 1, Enabled = builder.Configuration.GetValue("SubscriberService:Enabled", false) });
        await database.SaveChangesAsync();
    }
}
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapControllers();
app.Run();
