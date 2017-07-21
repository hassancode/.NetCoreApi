using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;

namespace TestApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Framework Services: like MVC, Entity Framework, Identity etc
            //Application Services: like EmailService
            //BuiltIn Service: like ApplicationBuilder, Logging.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //PIPELINE or MIDDLEWARE like HttpRequest handler (old asp.net used to have Modules/Handlers to handle this)
            // Sequence of Request, 1->2->3... Order MATTERS .
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) // We can do else, for Production env (which we can set through Project Property Env Variable), that is accessible through IHostingEnvironment
            {
                //Nice exception page will be shown as Developer friendly
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages(); //for browser showing HTTP Codes 

            app.UseMvc();//MVC - > View is Representaion of data (i.e. Json)

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            
        }

        //public void ConfigureRoutes(IRouteBuilder routeBuilder)
        //{
        //    routeBuilder.MapRoute("Default", "api/{controller}=Home/{action}=Index/{id?}");
        //}
    }
}
