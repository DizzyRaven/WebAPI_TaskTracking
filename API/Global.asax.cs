
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

//using Ninject;
//using Ninject.Modules;
//using Ninject.Web.Mvc;

//using API.Util;
using BLL.Infrastructure;


namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Dependency Injection 
            //    NinjectModule taskModule = new TaskModule();
            //    NinjectModule serviceModule = new ServiceModule("defaultConnection");
            //    var kernel = new StandardKernel(taskModule, serviceModule);
            //    DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
