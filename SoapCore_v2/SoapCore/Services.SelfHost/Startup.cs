using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Services;
using SoapCore;

namespace Services.SelfHost
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ZipService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            app.UseSoapEndpoint<ZipService>("/ZipService.svc", new System.ServiceModel.BasicHttpBinding(),SoapSerializer.DataContractSerializer);
            app.UseSoapEndpoint<ZipService>("/ZipService.asmx", new System.ServiceModel.BasicHttpBinding(), SoapSerializer.XmlSerializer);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Services Started!");
            });
        }
    }
}
