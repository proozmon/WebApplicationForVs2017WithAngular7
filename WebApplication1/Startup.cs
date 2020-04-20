using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace WebApplication1
{
    public class Startup
    {
        public void Register(HttpConfiguration config)
        {
            // call method to instantiate Web API routes
            config.MapHttpAttributeRoutes();

            // define parameters
            var name = "DefaultApi";
            var routeTemplate = "api/{controller}/{userName, userID}";
            string[] defaults = { "userName", "userID" };

            //  cast thign1 as an object with the names of the http  call and the parameters
            //  System.Collections.Generic.IDictionary<string, object> defaults =
            //    new System.Collections.Generic.Dictionary<string, object>
            //    {
            //      {
            //        "delete", (object) new thing1 { param1 = "userName", param2 = "userID" }
            //      }
            //    };

            //  cast thing2 as a dummy object
            //  System.Collections.Generic.IDictionary<string, object> constraints = 
            //    new System.Collections.Generic.Dictionary<string, object>
            //    {
            //      {
            //        "dummy1", (object) new thing2 { }
            //      }
            //    };

            //  here's an alternative but equivalent way to cast thing2 as a dummy object
            //  // (note that the explicit cast is actually redundant, sicne it's implicit here)
            //  var thing3 = (object) new thing2 { };
            //  System.Collections.Generic.IDictionary<string, object> dataTokens =
            //    new System.Collections.Generic.Dictionary<string, object>
            //    {
            //      { "dummy2", thing3 }
            //    };

            //  configure the mapping
            var routes = config.Routes;
            routes.MapHttpRoute(name, routeTemplate, defaults);
            //  config.Routes.CreateRoute(routeTemplate, defaults, constraints, dataTokens);
        }

        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //System.Action<HttpConfiguration> config;
            //config = null;

            var config = new HttpConfiguration();

            this.Register(config);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
