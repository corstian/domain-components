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
        .ConfigureLogging(logging => logging.AddConsole())
        .AddSimpleMessageStreamProvider("stream")
        //.AddMemoryGrainStorageAsDefault()
        .AddAzureBlobGrainStorageAsDefault(options =>
        {
            options.UseFullAssemblyNames = false;
            options.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            options.UseJson = true;
            options.IndentJson = true;
            options.ConfigureBlobServiceClient("DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;");
        })
        .AddLogStorageBasedLogConsistencyProvider()
        .AddMemoryGrainStorage("PubSubStore"))
    .Build();

await host.StartAsync();

// Get the grain factory
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

var guid = Guid.Parse("86308691-e97b-45f7-b1b3-8f9765bf3605");

var user = grainFactory.GetGrain<IAggregateGrain<User>>(guid);

var command = new ChangeInfo
{
    Name = guid.ToString(),
    Email = "john.doe@example.com"
};

var result = await user.EvaluateTypedCommand(command);

if (result.IsSuccess)
    await user.Apply(result.Value.Item1, result.Value.Item2);

Console.WriteLine("Press Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();