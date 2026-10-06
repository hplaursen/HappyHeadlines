using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace Monitoring;

public static class TracingService
{
    public static readonly string ServiceName =
        Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";

    public static readonly ActivitySource ActivitySource =
        new ActivitySource(ServiceName);

    public static TracerProvider Configure()
    {
        return Sdk.CreateTracerProviderBuilder()
            .AddSource(ActivitySource.Name)
            .AddAspNetCoreInstrumentation()
            .AddZipkinExporter(options =>
            {
                options.Endpoint =
                    new Uri("http://zipkin:9411/api/v2/spans");
            })
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(ServiceName))
            .Build();
    }
}