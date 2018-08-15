using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using System;
using Ninject.Activation;
using System.Collections.Generic;
using Ninject.Parameters;
using System.Linq;
namespace TaskTrackingAPI.Util
{
    public class NinjectResolver : NinjectScope, IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }

    public class NinjectScope : IDependencyScope
    {
        protected IResolutionRoot resolutionRoot;
        public NinjectScope(IResolutionRoot kernel)
        {
            resolutionRoot = kernel;
        }
        public object GetService(Type serviceType)
        {
            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return resolutionRoot.Resolve(request).SingleOrDefault();
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            IRequest request = resolutionRoot.CreateRequest(serviceType, null, new Parameter[0], true, true);
            return resolutionRoot.Resolve(request).ToList();
        }
        public void Dispose()
        {
            IDisposable disposable = (IDisposable)resolutionRoot;
            if (disposable != null) disposable.Dispose();
            resolutionRoot = null;
        }
    }
}