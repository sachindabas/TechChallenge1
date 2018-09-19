using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Web.Http.Routing;
using NumberInWords.Core;
using CurrencyToWord.Repository;
using CurrencyToWordsWeb.Resolver;
using CurrencyToWord.Service;
using System.Web.Http;
using System.Web.Http.Routing;

namespace CurrencyToWordsWeb
{
    public static class WebApiConfig
    {
        public static IUnityContainer Container
        {
            get; set;
        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services 
            var container = new UnityContainer();
            //Base Registrations Begin
            container.RegisterType<ILog>(new InjectionFactory(x => null));
            container.RegisterType<ILogger, FileLogger>();
            container.RegisterType<ICacheManager, HttpCacheManager>();
            //Base Registrations End

            container.RegisterType<IConversionService, ConversionService>();
            container.RegisterType<IConversionRepository, ConversionRepository>();
            Container = container;
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            config.AddApiVersioning();

            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };
            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public static T Resolve<T>()
        {
            T ret = default(T);

            if (Container.IsRegistered(typeof(T)))
            {
                ret = Container.Resolve<T>();
            }

            return ret;
        }

        /// <summary>
        /// Resolves the named registration of type T to the mentioned type
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="name">named registration</param>
        /// <returns></returns>
        public static T Resolve<T>(string name)
        {
            var ret = Container.Resolve<T>(name);
            return ret;
        }
    }
}
