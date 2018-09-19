using System;
using System.Web;
using NUnit.Framework;

namespace NumberInWords.Core.Test
{
    [TestFixture]
    public class CacheManagerTest
    {
        [Test]
        public void TestAddInCache()
        {
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            cacheManager.Add<string>("testkey", "testvalue", TimeSpan.Parse("1"));
            var result = cacheManager.Get<string>("testkey");
            Assert.IsNull(result);
        }

        [Test]
        public void TestAddInCacheUsingTimeSpan()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            cacheManager.Add<string>("testkey", "testvalue", TimeSpan.FromMinutes(1));
            var result = cacheManager.Get<string>("testkey");
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestIskeyValid()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            cacheManager.Add<string>("testkey", "testvalue", TimeSpan.FromMinutes(1));
            var result = cacheManager.IsKeyExists("testkey");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestRemoveKeyFromCache()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            cacheManager.Add<string>("testkey", "testvalue", TimeSpan.FromMinutes(1));
            var result = cacheManager.IsKeyExists("testkey");
            Assert.IsTrue(result);
            cacheManager.Remove("testkey");
            Assert.IsFalse(cacheManager.IsKeyExists("testkey"));
        }
    }
}
