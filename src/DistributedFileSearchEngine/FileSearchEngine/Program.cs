using FileSearchEngine;
using FileSearchEngine.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;

await Host.CreateDefaultBuilder(args)
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
        builder.AddMemoryGrainStorageAsDefault();
        builder.AddSimpleMessageStreamProvider("SMS");
        builder.AddMemoryGrainStorage("PubSubStore");
    })
    .ConfigureWebHostDefaults(
        webBuilder => webBuilder.UseStartup<Startup>())
    .RunConsoleAsync();