using CurrencyToWord.Entities;

namespace CurrencyToWord.Service
{
    public interface IConversionService
    {
        /// <summary>
        /// convert to words
        /// </summary>
        /// <param name="input">input received</param>
        /// <returns>OutputModel</returns>
        OutputModel ConvertToWord(InputModel input);
    }
}
