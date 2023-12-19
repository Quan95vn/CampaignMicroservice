using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.CommandHandler;
using UserManagement.Application.EventHandler;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext (use in-memory database for simplicity)
builder.Services.AddDbContext<UserContext>(options =>
    options.UseInMemoryDatabase("CampaignsDb"));

// Add repository to DI container
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add MediatR and specify the assembly where the handlers are located
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));

// Add MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserEligibleForPrizeConsumer>();
    //docker run -d --hostname my-rabbitmq-server --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
    //localhost:15672, guest/guest
    x.UsingRabbitMq((context, configurator) =>
    {
        configurator.ConfigureEndpoints(context);
    });
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