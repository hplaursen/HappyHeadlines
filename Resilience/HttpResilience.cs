using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace Resilience;

public static class HttpCircuitBreaker
{
    public static void Configure(
        ResiliencePipelineBuilder<HttpResponseMessage> builder)
    {
        builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
        {
            FailureRatio = 1.0,
            MinimumThroughput = 3,
            SamplingDuration = TimeSpan.FromSeconds(10),
            BreakDuration = TimeSpan.FromSeconds(30)
        });
    }
}