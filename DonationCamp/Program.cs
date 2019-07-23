using System;
using System.Linq;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Prometheus;

namespace DonationCamp

{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            //Metrics = AppMetrics.CreateDefaultBuilder()
            //        .OutputMetrics.AsPrometheusPlainText()
            //        .OutputMetrics.AsPrometheusProtobuf()
            //        .Build();

            return WebHost.CreateDefaultBuilder(args)
                            .Configure(app => app.UseMetricServer())
                //            .UseMetrics(
                //             options => {
                //                options.EndpointOptions = endpointsOptions => {
                //                    endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                //                };
                //})
                            .UseStartup<Startup>()
                            .Build();
        }
    }
}
