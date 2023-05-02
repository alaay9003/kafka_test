using Confluent.Kafka;
using ConsumerApp.Models;
using ConsumerApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



var consumerConfig = new ConsumerConfig();
builder.Configuration.Bind("consumer", consumerConfig);
builder.Services.AddSingleton<ConsumerConfig>(consumerConfig);


builder.Services.AddTransient<IConsumerService, ConsumerService>();
builder.Services.AddSingleton<BackgroundService, ConsumerTest>();
//builder.Services.AddHostedService<ConsumerTest>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var Connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Connection)
);


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
