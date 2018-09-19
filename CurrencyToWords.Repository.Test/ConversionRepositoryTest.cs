using System;
using NumberInWords.Core;
using NUnit.Framework;

namespace CurrencyToWord.Repository.Test
{
    [TestFixture]
    public class ConversionRepositoryTest
    {
        #region Test Cases For Only Integer
        
        [Test]
        public void ConvertToCurrency()
        {
            ILogger logger = new FileLogger();
            IConversionRepository sut = new ConversionRepository(logger);
            string expectedResult = sut.ConvertToWord("7");
            Assert.That(expectedResult.Trim().ToUpper().Equals("SEVEN DOLLARS"));
        }

        
        #endregion
    }
}
