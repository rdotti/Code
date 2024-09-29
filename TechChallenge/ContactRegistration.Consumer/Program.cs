using ContactConsumer.Consumer.Configurations;
using ContactConsumer.Consumer.Infra;
using ContactConsumer.Infrastructure.Configurations;
using ContactConsumer.Domain.Configurations;
using Shared.Rabbit.Configurations;
using ContactConsumer.Domain.Models;
using Shared.Domain.Models;

var builder = Host.CreateApplicationBuilder(args);

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


var host = builder.Build();
host.Run();
