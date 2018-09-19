using System;
using System.Web;
using System.Web.Caching;

namespace NumberInWords.Core
{
    public class HttpCacheManager : ICacheManager 
    {
        readonly ILogger _logger;
        public HttpCacheManager(ILogger logger)
        {
            _logger = logger;
        }

        public void Add<T>(string key, T obj, TimeSpan timeSpan) where T : class
        {
            _logger.WriteDebug("Add cache method invoked.");
            try
            {
                HttpContext.Current.Cache.Insert(key, obj, null, DateTime.UtcNow.Add(timeSpan), Cache.NoSlidingExpiration);
                _logger.WriteDebug("Add cache method completed.");
            }
            catch (Exception ex)
            {
                _logger.WriteError(string.Format("Add cache method: Exception occured after all of the retries failed while adding key - {0}", key), ex);
            }
        }

        public T Get<T>(string key) where T : class
        {
            _logger.WriteDebug("Get cache method invoked.");
            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    return (T)HttpContext.Current.Cache.Get(key);
                }
                catch (Exception exc)
                {
                    _logger.WriteError(string.Format("Get cache method: Exception occured after all of the retries failed while Getting value for key - {0}", key), exc);
                }
            }
            return default(T);

        }

        public T Get<T>(string key, params object[] args) where T : class
        {
            _logger.WriteDebug("Get cache method invoked.");
            return (T)HttpContext.Current.Cache.Get(string.Format(key, args));
        }

        /// <summary>
        /// This method returns false if key exists. It is written like this to avoid any more changes as existing implementation
        /// is like this only.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyExists(string key)
        {
            _logger.WriteDebug("IsKeyExists method invoked.");
            bool valid = true;
            try
            {
                if (HttpContext.Current.Cache.Get(key) != null)
                    valid = false;
            }
            catch (Exception exc)
            {
                _logger.WriteError(string.Format("IsKeyExists method: Exception occured after all of the retries failed while checking for key - {0} exists", key), exc);
                valid = true;
            }
            _logger.WriteDebug("IsKeyExists method completed.");
            return !valid;
        }

        public void Remove(string key)
        {
            _logger.WriteDebug("Remove method invoked.");
            if (!string.IsNullOrEmpty(key) && IsKeyExists(key))
            {
                try
                {
                    HttpContext.Current.Cache.Remove(key);
                }
                catch (Exception ex)
                {
                    _logger.WriteError(string.Format("Remove method: Exception occured after all of the retries failed while removing key - {0} ", key), ex);
                }
            }
            _logger.WriteDebug("Remove method completed.");
        }
    }
}
