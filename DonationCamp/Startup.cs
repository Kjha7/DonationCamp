using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DonationCamp.Configs;
using DonationCamp.Services;
using Prometheus;

namespace DonationCamp

{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbConfig>(Configuration.GetSection(nameof(MongoDbConfig)));
            services.Configure<LoginConfig>(Configuration.GetSection(nameof(LoginConfig)));
            services.AddSingleton<DonationServices>();
            services.AddSingleton<SessionServices>();
            services.AddHttpContextAccessor();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(10);//You can set Time
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseMetricsTextEndpoint();
            //app.UsePrometheusServer();

            var instance = Environment.MachineName;
            string envir = "Dev";

            var counter = Metrics.CreateCounter("Number_of_request_Services", "Counts requests to endpoints", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "http_status", "instance", "env" }
            });
            app.Use((context, next) =>
            {
                counter.WithLabels(context.Request.Method, context.Request.Path, context.Response.StatusCode.ToString(), instance, envir).Inc();
                return next();
            });
            app.UseMetricServer();
            
            //app.UseHttpMetrics();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
        }
    }
}
