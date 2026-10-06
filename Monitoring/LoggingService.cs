using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Monitoring;

public static class LoggingService
{
    public static void Configure(WebApplicationBuilder builder)
    {
        string seqUrl =
            builder.Environment.IsDevelopment()
                ? "http://localhost:5341"
                : "http://seq:5341";


        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .WriteTo.Console()
                .WriteTo.Seq(seqUrl);
        });
    }
}