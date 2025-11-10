using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

// Add Services personalizados
builder.Services.AddTransient<IMeuServico, MeuServico>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection"); // DefaultConnection é o nome da connection string no appsettings.json

// acessar valor de 'appsettings.json': var valor1 = builder.Configuration["chave1"];

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection, 
                    ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler(); // middleware de tratamento de exceções
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Mapeia Atributos dos Controllers Existentes
app.MapControllers();

app.Run();
