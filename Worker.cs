namespace tvmaze_worker
{
  public class Worker : IHostedService
  {
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
      _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
      await Task.Delay(1000, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}