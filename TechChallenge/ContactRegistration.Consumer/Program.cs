using ContactConsumer.Consumer.Configurations;
using ContactConsumer.Infrastructure.Configurations;
using ContactConsumer.Domain.Configurations;
using Shared.Rabbit.Configurations;
using Shared.Domain.Models;
using Shared.Infraestructure.Configurations;
using Shared.Rabbit.Infra;

var builder = Host.CreateApplicationBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.AddRabbitMq(options =>
{
    options.HostName = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.Host);
    options.Port = builder.Configuration.GetValue<int>(ApplicationVariables.Rabbit.Port);
    options.Username = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.User);
    options.Password = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.Password);
    options.VirtualHost = builder.Configuration.GetValue<string>(ApplicationVariables.Rabbit.VirtualHost);
});

builder.Services.AddDeletedQueue(builder.Configuration);
builder.Services.AddUpdatedQueue(builder.Configuration);
builder.Services.AddInsertQueue(builder.Configuration);
builder.Services.AddDomain<DeleteContactModel>();
builder.Services.AddDomain<InsertContactModel>();
builder.Services.AddDomain<UpdateContactModel>();
builder.Services.AddRepository();
builder.Services.ConfigureDatabase(configuration.GetConnectionString("TechChallenge") ?? string.Empty);


var host = builder.Build();
host.Run();
