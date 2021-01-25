using Autofac;
using GlobalX.Coding.Assessment.BackgroundTasks;
using GlobalX.Coding.Assessment.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GlobalX.Coding.Assessment
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomHealthCheck(Configuration)
                .Configure<GlobalXConfig>(Configuration.GetSection("GlobalXConfig"))
                .AddHostedService<MainTask>()
                .AddOptions();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Use dependency injection here in "ApplicationModule" Class
            builder.RegisterModule(new ApplicationModule(Configuration["p"]));
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
