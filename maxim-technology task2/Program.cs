using DeliverySystem.Services;
using maxim_technology_task2;
using maxim_technology_task2.endpoints;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Map>();
builder.Services.AddSingleton<DriverFactory>();
var app = builder.Build();

var factory = app.Services.GetRequiredService<DriverFactory>();
factory.CreateDrivers();

app.PutDriverCoordinates();

app.Run();
