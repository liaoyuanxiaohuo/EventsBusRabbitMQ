using EventBus.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 注入事件处理服务
//builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrderEto>), typeof(CreateOrderEventBusHandle));
//builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrder1Eto>), typeof(CreateOrder1EventBusHandle));

//rabbitmq配置
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(nameof(RabbitMQOptions)));

// 注入RabbitMQ服务
builder.Services.AddEventBusRabbitMQ();

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
