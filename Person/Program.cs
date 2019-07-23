using System;
using System.Linq;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Prometheus;

namespace PersonDocument
{
    public class Program
    {
        public static IMetricsRoot Metrics { get; set; }
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .Configure(app => app.UseMetricServer())
                            //.UseMetrics(
                            // options => {
                            //     options.EndpointOptions = endpointsOptions => {
                            //         endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                            //     };
                            // })
                            .UseStartup<Startup>()
                            .Build();
        }
    }
}
