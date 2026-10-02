using Polly;
using Polly.Retry;

namespace Resilience;

public static class DatabaseRetry
{
    public static async Task ExecuteAsync(
        Func<CancellationToken, ValueTask> action)
    {
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions
            {
                MaxRetryAttempts = 10,
                Delay = TimeSpan.FromSeconds(3)
            })
            .Build();

        await pipeline.ExecuteAsync(action);
    }
}