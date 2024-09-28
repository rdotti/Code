using TechChallenge.Infraestructure.Configurations;
using TechChallenge.Queue.Consumer;

var builder = Host.CreateApplicationBuilder(args);
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

builder.Services.ConfigureDatabase(configuration.GetConnectionString("TechChallenge") ?? string.Empty);
builder.Services.AddHostedService<Worker>();


var host = builder.Build();
host.Run();
