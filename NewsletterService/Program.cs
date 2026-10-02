using EasyNetQ;
using NewsletterService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddHostedService<ArticleQueueSubscriber>();
builder.Services.AddScoped<NewsletterManager>();
builder.Services.AddEasyNetQ(builder.Configuration.GetConnectionString("articleQueueClient"));

builder.Services.AddHttpClient<ArticleClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("articleClient")!); 
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();