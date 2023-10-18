using ProductLib;

public class InitService : IHostedService
{
    public  Task StartAsync(CancellationToken cancellationToken)
    {
        new ProductService().Initialize();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

