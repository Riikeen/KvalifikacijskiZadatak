using Data;
using Data.Interfaces;
using Data.Repositories;
using BussinesLogic.Interfaces;
using BussinesLogic.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddControllers().AddJsonOptions(x =>
//{
//    //enums in api responses are strings
//    //input has to be int 
//    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory,DbConnectionFactory>();
builder.Services.AddScoped<IDbService,DbService>();

builder.Services.AddScoped<IStrojeviService, StrojeviService>();
builder.Services.AddScoped<IKvaroviService, KvaroviService>();

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
