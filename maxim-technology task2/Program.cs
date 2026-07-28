using DeliverySystem.Algorithms;
using DeliverySystem.Services;
using maxim_technology_task2;
using maxim_technology_task2.endpoints;
using maxim_technology_task2.Middlewares;
using maxim_technology_task2.Services;


var builder = WebApplication.CreateBuilder(args);

int m = builder.Configuration.GetValue<int>("MapSettings:M",50);
int n = builder.Configuration.GetValue<int>("MapSettings:N",50);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Map>(map=> new Map(m,n));
builder.Services.AddSingleton<DriverFactory>();
builder.Services.AddSingleton<IAlgorithm, GridSearchAlgorithm>();
builder.Services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
{
    client.Timeout = TimeSpan.FromMilliseconds(500);
});
builder.Services.AddTransient<OrderService>();


var app = builder.Build();

var factory = app.Services.GetRequiredService<DriverFactory>();
factory.CreateDrivers();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<MiddlewareParallelLimit>();
app.UseMiddleware<MiddlewareException>();
app.PutDriverCoordinates();
app.PostCreateOrder();
app.Run();
