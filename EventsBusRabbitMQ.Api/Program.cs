using EventBus.RabbitMQ;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ע���¼��������
//builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrderEto>), typeof(CreateOrderEventBusHandle));
//builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrder1Eto>), typeof(CreateOrder1EventBusHandle));

//rabbitmq����
//builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(nameof(RabbitMQOptions)));

// ע��RabbitMQ����
builder.Services.AddEventBusRabbitMQ(option =>
{
    option.HostName = builder.Configuration.GetSection("RabbitMQOptions:HostName").Value ?? "192.168.8.116";
    option.Port = int.Parse(builder.Configuration.GetSection("RabbitMQOptions:Port").Value ?? "30135");
    option.UserName = builder.Configuration.GetSection("RabbitMQOptions:UserName").Value!;
    option.Password = builder.Configuration.GetSection("RabbitMQOptions:Password").Value!;
});

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
