using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application.CommandHandler;
using OrderProcessing.Infrastructure;
using OrderProcessing.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext (use in-memory database for simplicity)
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseInMemoryDatabase("OrdersDb"));

// Add repository to DI container
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Add MediatR and specify the assembly where the handlers are located
builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));

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