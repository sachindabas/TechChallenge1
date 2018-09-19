using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace CurrencyToWordsWeb.Resolver
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }

        /// <summary>
        /// Resolves the type parameter T to an instance of the appropriate type.
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        public T Resolve<T>()
        {
            T ret = default(T);

            if (container.IsRegistered(typeof(T)))
            {
                ret = container.Resolve<T>();
            }

            return ret;
        }

        /// <summary>
        /// Resolves the named registration of type T to the mentioned type
        /// </summary>
        /// <typeparam name="T">Type of object to return</typeparam>
        /// <param name="name">named registration</param>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            var ret = container.Resolve<T>(name);
            return ret;
        }
    }
}