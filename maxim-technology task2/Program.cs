using DeliverySystem.Services;
using maxim_technology_task2;
using maxim_technology_task2.endpoints;


var builder = WebApplication.CreateBuilder(args);

int m = builder.Configuration.GetValue<int>("MapSettings:M",50);
int n = builder.Configuration.GetValue<int>("MapSettings:N",50);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Map>(map=> new Map(m,n));

builder.Services.AddSingleton<DriverFactory>();
var app = builder.Build();

var factory = app.Services.GetRequiredService<DriverFactory>();
factory.CreateDrivers();

app.PutDriverCoordinates();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
