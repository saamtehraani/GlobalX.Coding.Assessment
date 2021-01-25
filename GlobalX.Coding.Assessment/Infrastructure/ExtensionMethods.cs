using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace GlobalX.Coding.Assessment.Infrastructure
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            return services;
        }

        public static ILoggingBuilder UseSerilog(this ILoggingBuilder builder, IConfiguration configuration)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
            string logName = $"{configuration["Serilog:FilePath"]}\\GlobalX.Assessment {DateTime.Now:yyyy-MM-dd}.log";
            Log.Logger = loggerConfiguration
                .MinimumLevel.Information()
                .Enrich.WithProperty("ApplicationContext", Program.AppName)
                .Enrich.FromLogContext()
                .WriteTo.File(logName,
                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                rollOnFileSizeLimit: true)
                .CreateLogger();

            return builder;
        }
    }
}
