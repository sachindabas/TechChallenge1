using Microsoft.Security.Application;
using NumberInWords.Core;
using CurrencyToWord.Entities;
using CurrencyToWord.Repository;
using System;

namespace CurrencyToWord.Service
{
    public class ConversionService : IConversionService
    {
        readonly ILogger _logger;
        readonly IConversionRepository _conversionRepository;
        readonly ICacheManager _cacheManager;

        public ConversionService(ILogger logger, IConversionRepository conversionRepository, ICacheManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
            _conversionRepository = conversionRepository;
        }

        public OutputModel ConvertToWord(InputModel input)
        {
            _logger.WriteDebug("ConversionService - Convert: Method Started");
            var result = new OutputModel();
            if (input != null)
            {
                result.Name = Sanitizer.GetSafeHtmlFragment(input.Name);
                var cacheKey = string.Format(Constants.CacheKey, Convert.ToString(input.Number).Replace(".", "-"));
                if (_cacheManager.IsKeyExists(cacheKey))
                {
                    result.NumberInWords = _cacheManager.Get<string>(cacheKey);
                }
                else
                {
                    result.NumberInWords = _conversionRepository.ConvertToWord(Convert.ToString(input.Number));
                    _cacheManager.Add(cacheKey, result.NumberInWords, TimeSpan.FromHours(Convert.ToDouble(Constants.DefaultCacheDuration)));
                }
            }
            _logger.WriteDebug("ConversionService - Convert: Method Ended");
            return result;
        }

        
    }
}
