using EventBus.RabbitMQ;

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
builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(nameof(RabbitMQOptions)));

// ע��RabbitMQ����
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
