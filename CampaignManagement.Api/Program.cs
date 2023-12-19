using System.Reflection;
using CampaignManagement.Application.CommandHandler;
using CampaignManagement.Infrastructure;
using CampaignManagement.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR and specify the assembly where the handlers are located
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Configure DbContext (use in-memory database for simplicity)
builder.Services.AddDbContext<CampaignContext>(options =>
    options.UseInMemoryDatabase("CampaignsDb"));

// Add repository to DI container
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();

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