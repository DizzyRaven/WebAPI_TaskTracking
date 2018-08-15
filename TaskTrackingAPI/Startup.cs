using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Reflection;

[assembly: OwinStartup(typeof(TaskTrackingAPI.Startup))]

namespace TaskTrackingAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //app.UseNinjectMiddleware(CreateKernel);
            // app.UseNinjectWebApi(webApiConfiguration);
        }
        //public void Configuration(IAppBuilder app)
        //{
        //    var webApiConfiguration = new HttpConfiguration();
        //    webApiConfiguration.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional});

        //    ConfigureAuth(app);
        //    app.UseNinjectMiddleware(CreateKernel);
        //    app.UseNinjectWebApi(webApiConfiguration);
        //}

        ///// <summary>
        ///// Creates the kernel.
        ///// </summary>
        ///// <returns>the newly created kernel.</returns>
        //private static StandardKernel CreateKernel()
        //{
        //    var kernel = new StandardKernel();
        //    kernel.Load(Assembly.GetExecutingAssembly());
        //    return kernel;
        //}
    }
}
