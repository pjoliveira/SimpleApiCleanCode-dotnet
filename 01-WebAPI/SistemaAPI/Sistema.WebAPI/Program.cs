using Microsoft.Extensions.DependencyInjection;
using SistemaAPI.Data.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// metodos de extensão para Inversao De Controle
//**************************************************************************************
builder.Services.AddConnection(builder.Configuration);  //conexão com banco de dados
builder.Services.AddRespositorios();                    // repositorios do projeto 
//builder.Services.AddAutenticacao();                     // autenticação 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
