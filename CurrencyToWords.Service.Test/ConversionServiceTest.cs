using System;
using System.Web;
using NumberInWords.Core;
using CurrencyToWord.Repository;
using NUnit.Framework;

namespace CurrencyToWord.Service.Test
{
    [TestFixture]
    public class ConversionServiceTest
    {
        [Test]
        public void GetCurrencyFromCache()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            IConversionRepository conversionRepository = new ConversionRepository(logger);
            IConversionService conversionService = new ConversionService(logger, conversionRepository, cacheManager);
            cacheManager.Add<string>("11_Currency", "11Test",TimeSpan.FromMinutes(1));
            var expectedResult = conversionService.ConvertToWord(new Entities.InputModel() { Number = 11 });
            Assert.That(expectedResult.NumberInWords.Equals("11Test"));
        }

        [Test]
        public void GetCurrencyFromRepository()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
            ILogger logger = new FileLogger();
            ICacheManager cacheManager = new HttpCacheManager(logger);
            IConversionRepository conversionRepository = new ConversionRepository(logger);
            IConversionService conversionService = new ConversionService(logger, conversionRepository, cacheManager);
            cacheManager.Add<string>("11_Currency", "11Test", TimeSpan.FromMinutes(1));
            var expectedResult = conversionService.ConvertToWord(new Entities.InputModel() {  Number = 11 });
            Assert.That(expectedResult.NumberInWords.Equals("11Test"));
            cacheManager.Remove("11_Currency");
            expectedResult = conversionService.ConvertToWord(new Entities.InputModel() {  Number = 11 });
            Assert.That(expectedResult.NumberInWords.ToUpper().Trim().Equals("ELEVEN DOLLARS"));
        }

        
    }
}
