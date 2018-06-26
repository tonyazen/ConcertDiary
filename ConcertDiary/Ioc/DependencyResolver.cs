using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.WebApi;


namespace ConcertDiary.Ioc
{
    public class DependencyResolver : IDependencyResolver
    {
        private UnityDependencyResolver Resolver { get; }

        public DependencyResolver(IUnityContainer container)
        {
            Resolver = new UnityDependencyResolver(container);
        }

        public void Dispose()
        {
            Resolver.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return Resolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Resolver.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return Resolver.BeginScope();
        }
    }
}