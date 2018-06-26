using System.Web.Http;

namespace ConcertDiary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.DependencyResolver = UnityConfig.GetDependencyResolver();

            //var logger = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILog)) as LogManager;
            //if (logger == null)
            //{
            //    throw new DependencyMissingException("Logger type not defined.");
            //}

        }
    }
}
