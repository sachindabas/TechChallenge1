using NumberInWords.Core;

namespace CurrencyToWord.Repository
{
    public interface IConversionRepository
    {
        /// <summary>
        /// convert the decimal in words
        /// </summary>
        /// <param name="number">number</param>
        /// <param name="isCurrency">isCurrency</param>
        /// <returns></returns>
        string ConvertToWord(string number);
    }
}
