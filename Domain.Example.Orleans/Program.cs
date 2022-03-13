using Domain.Components.Extensions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

using var host = new HostBuilder()
    .UseOrleans(builder => builder
        .UseLocalhostClustering()
        .AddMemoryGrainStorageAsDefault()
        .ConfigureLogging(logging => logging.AddConsole())
        .AddSimpleMessageStreamProvider("stream"))
    .Build();

await host.StartAsync();

// Get the grain factory
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

var user = grainFactory.GetGrain<IAggregateGrain<User>>(Guid.NewGuid());

var command = new ChangeEmail
{
    //Email = "john.doe@example.com"
};

var result = await user.EvaluateTypedCommand(command);

if (result.IsSuccess)
{
    await user.Apply(result.Value);
}

Console.WriteLine("Press Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();