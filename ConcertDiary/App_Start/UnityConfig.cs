using System.Web.Http.Dependencies;
using ConcertDiary.Ioc;
using ConcertDiary.Repository;
using ConcertDiary.Services;
using Unity;
using UnityLog4NetExtension.Log4Net;

namespace ConcertDiary
{
    public static class UnityConfig
    {
        private static UnityContainer _container;
        private static UnityContainer Container => _container ?? (_container = InitializeContainer());

        private static UnityContainer InitializeContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IConcertService, ConcertService>();
            container.RegisterType<ITweetService, TweetService>();
            container.RegisterType<IConcertRepo, ConcertRepo>();

            return container;
        }

        public static IDependencyResolver GetDependencyResolver()
        {
            return new DependencyResolver(Container);
        }
    }
}