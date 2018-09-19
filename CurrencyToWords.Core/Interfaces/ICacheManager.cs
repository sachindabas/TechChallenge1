using System;

namespace NumberInWords.Core
{
    public interface ICacheManager
    {
        /// <summary>
        /// This method is to add values to cache with timespan.
        /// </summary>
        /// <param name="key"> cache key name</param>
        /// <param name="value"> caching value</param>
        /// <param name="dependency"> caching timespan</param>
        void Add<T>(string key, T obj, TimeSpan timeSpan) where T : class;

        /// <summary>
        /// This method is to get cached object 
        /// </summary>
        /// <param name="key"> cache key name </param>
        /// <returns>returns cached object </returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// This method is to check cache key exists in redis cache or not
        /// </summary>
        /// <param name="key"> cache key name</param>
        /// <returns> returns boolean value </returns>
        bool IsKeyExists(string key);

        /// <summary>
        /// This method is to remove cached object
        /// </summary>
        /// <param name="key"> cahce key name</param>
        void Remove(string key);
    }
}
