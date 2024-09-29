using Microsoft.OpenApi.Models;
using System.Reflection;
using ContactRegistration.Domain.Configurations;
using ContactRegistration.Infrastructure.Configurations;
using Shared.Rabbit.Configurations;
using Shared.Rabbit.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TechChallenge" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.ConfigurationDomain();
builder.Services.ConfigurationRepository();
builder.Services.ConfigurationRabbitProducer(options =>
{
    options.HostName = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.Host);
    options.Port = builder.Configuration.GetValue<int>(ApplicationVariables.Rabbit.Port);
    options.Username = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.User);
    options.Password = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.Password);
    options.VirtualHost = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.VirtualHost);
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
