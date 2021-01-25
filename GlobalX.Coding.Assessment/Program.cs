using Autofac.Extensions.DependencyInjection;
using GlobalX.Coding.Assessment.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Net;

namespace GlobalX.Coding.Assessment
{
    class Program
    {
        public static readonly string AppName = typeof(Program).Assembly.GetName().Name;
        public static int Main(string[] args)
        {
            try
            {
                var configuration = GetConfiguration();
                var host = CreateHostBuilder(configuration, args);
                Log.Information("Starting host ({ApplicationContext})...", AppName);

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHost CreateHostBuilder(IConfiguration configuration, string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options =>
                    {
                        var port = configuration.GetValue("Port", 81);
                        options.Listen(IPAddress.Any, port, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });
                    });
                })
                .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                .ConfigureAppConfiguration((host, builder) =>
                {
                    builder.AddConfiguration(configuration);
                    builder.AddCommandLine(args);
                })
                .ConfigureLogging((host, builder) => builder.UseSerilog(host.Configuration).AddSerilog())
                .Build();
        }
        private static IConfiguration GetConfiguration()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
