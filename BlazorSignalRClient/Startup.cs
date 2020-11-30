using BlazorSignalRClient.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast;
using System.Net.Http;

namespace BlazorSignalRClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //Func<HttpMessageHandler> configureHandler = () =>
            //{
            //    var bypassCertValidation = Configuration.GetValue<bool>("BypassRemoteCertificateValidation");
            //    var handler = new HttpClientHandler();
            //    //!DO NOT DO IT IN PRODUCTION!! GO AND CREATE VALID CERTIFICATE!
            //    if (bypassCertValidation)
            //    {
            //        handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, x509Certificate2, x509Chain, sslPolicyErrors) =>
            //        {
            //            return true;
            //        };
            //    }
            //    return handler;
            //};


            services.AddHttpClient<Services.ApiService>(client =>
            {
                client.BaseAddress = new Uri("https://host.docker.internal:5001");
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            });

            

            services.AddBlazoredToast();

            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }


    }
}
