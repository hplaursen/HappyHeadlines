using EasyNetQ;
using PublisherService.Services;
using Monitoring;


var builder = WebApplication.CreateBuilder(args);

TracingService.Configure();
LoggingService.Configure(builder);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<ArticleQueuePublisher>();
builder.Services.AddScoped<PublisherManager>();
builder.Services.AddEasyNetQ(builder.Configuration.GetConnectionString("articleQueueClient"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();