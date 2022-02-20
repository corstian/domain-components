using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

using var host = new SiloHostBuilder()
    .UseLocalhostClustering()
    .AddMemoryGrainStorageAsDefault()
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.StartAsync();

// Get the grain factory
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

var user = grainFactory.GetGrain<IAggregateGrain<User>>(Guid.Empty);

var command = new ChangeEmail
{
    Email = "john.doe@example.com"
};

var result = await user.Evaluate(command);


Console.WriteLine("\n\n{0}\n\n", result);

Console.WriteLine("Press Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();