using WorkerServiceExample;
using WorkerServiceExample.TaskQueue;

bool IsService = Console.IsErrorRedirected;
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        services.AddHostedService<QueuedHostedService>();
        services.AddHostedService<WokerServiceTest>();
        //services.AddHostedService<Worker>();
    })
    .Build();
if (!IsService)
{
    Console.WriteLine("Worker service is starting....");
    Console.CancelKeyPress += (sender, e) =>
    {
        Console.WriteLine("Worker service is stopping...");
        host.StopAsync();
        e.Cancel = true;
    };
    Console.WriteLine("Press CTRL+C to stop...");
    await host.RunAsync();
}
else
{
    await host.StartAsync();
}
