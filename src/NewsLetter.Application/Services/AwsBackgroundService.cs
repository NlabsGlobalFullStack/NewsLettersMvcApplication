using Microsoft.Extensions.Hosting;

namespace NewsLetter.Application.Services;
public sealed class AwsBackgroundService(AwsAction awsAction) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await awsAction.ExecuteAsync("", stoppingToken);
    }
}
