using System.Data.Entity;
using Automobiliu_Nuoma_Web_Api.IRepositories;
using Automobiliu_Nuoma_Web_Api.IServices;
using Automobiliu_Nuoma_Web_Api.Repositories;
using Automobiliu_Nuoma_Web_Api.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddSingleton<IMongoDatabase, >

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Pridėti duomenų bazės konfigūraciją
//builder.Services.AddSingleton<DBConfiguration>();
// Pridėti repository
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
// Pridėti servisai
builder.Services.AddScoped<ICarService, CarService>(); 
builder.Services.AddScoped<IClientService, ClientService>(); 
builder.Services.AddScoped<IEmployeeService, EmployeeService>(); 
builder.Services.AddScoped<IRentalService, RentalService>();

builder.Services.AddScoped<IUtilsService, UtilsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // Log all HTTP requests

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
