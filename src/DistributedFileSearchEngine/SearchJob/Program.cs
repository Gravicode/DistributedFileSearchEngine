// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using SearchModels.Models;

namespace SearchJob;

public class Program
{
    static int Main(string[] args)
    {
        return RunMainAsync().Result;
    }

    private static async Task<int> RunMainAsync()
    {
        try
        {
            using (var client = await ConnectClient())
            {
                await DoClientWork(client);
                Console.ReadKey();
            }

            return 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"\nException while trying to run client: {e.Message}");
            Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
            return 1;
        }
    }

    private static async Task<IClusterClient> ConnectClient()
    {
        IClusterClient client;
        client = new ClientBuilder()
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "DevMachine";
                options.ServiceId = "FileService1";
            })
            .ConfigureLogging(logging => logging.AddConsole())
            .Build();

        await client.Connect();
        Console.WriteLine("Client successfully connected to silo host \n");
        return client;
    }

    private static async Task DoClientWork(IClusterClient client)
    {
        // get silo info
        var friend = client.GetGrain<IDeviceInfoGrain>(new Guid().ToString());
        var response = await friend.GetInfo();
        Console.WriteLine($"SILO MACHINE:\n{response}\n\n");
        //get file from some folder
        var crawler = client.GetGrain<IFileSearchGrain>(new Guid().ToString());
        var res = await crawler.SearchFile("*.sql",Folders.Documents);
        Console.WriteLine("seacrh for .sql in mydocument");
        if (res != null)
        {
            foreach(var file in res)
            {
                Console.WriteLine($"{file.Name} -> {file.Directory}");
            }
        }
        res = await crawler.SearchFile("*.jpg", Folders.Desktop);
        Console.WriteLine("\nseacrh for .sql in desktop");
        if (res != null)
        {
            foreach (var file in res)
            {
                Console.WriteLine($"{file.Name} -> {file.Directory}");
            }
        }


    }
}