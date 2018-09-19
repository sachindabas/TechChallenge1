using NumberInWords.Core;
using System;
using System.Linq;
using System.Text;

namespace CurrencyToWord.Repository
{
    public class ConversionRepository : IConversionRepository
    {
        readonly ILogger _logger;

        public ConversionRepository(ILogger logger)
        {
            _logger = logger;
        }

        #region Public Method        
        public string ConvertToWord(string number)
        {
            _logger.WriteDebug("ConversionService - Convert: Method Started");
            string isNegative = string.Empty;
            try
            {
                if (number.Contains("-"))
                {
                    isNegative = "Minus ";
                    number = number.Substring(1, number.Length - 1);
                }
                if (number == "0")
                {
                    return "Zero";
                }
                else
                {
                    return string.Format("{0}", isNegative + ConvertFullNumberToWord(number));
                }
            }
            catch (Exception ex)
            {
                _logger.WriteError("ConversionService - Convert: Method Failed. Error Details: " + ex.Message, ex);
                return ex.Message;
            }
        }

        #endregion

        #region Private Method

        /// <summary>
        /// ConvertFullNumberToWord
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static String ConvertFullNumberToWord(String number)
        {
            String result = string.Empty, decimalNumber = string.Empty, joiningString = string.Empty, pointStr = string.Empty;
            var numberParts = number.Split(new char[] { '.' });
            if (numberParts.Length > 1 && Convert.ToInt32(numberParts.Last()) > 0)
            {
                joiningString = " and";
                pointStr = string.Concat(ConvertNumberAfterDecimal(numberParts.Last()), " Cent");
            }
            string words = string.IsNullOrEmpty(numberParts.First()) ? string.Empty : ConvertIntegerNumber(numberParts.First()).Trim();

            result = String.Format("{0} {1} {2}", string.IsNullOrEmpty(words) ? string.Empty : words + " Dollars",
                string.IsNullOrEmpty(words) ? string.Empty : joiningString, pointStr.Trim());

            return result;
        }

        /// <summary>
        /// Method to Convert Integer Part of number into words
        /// </summary>
        /// <param name="Number">Input Number</param>
        /// <returns>Word Format of Number</returns>
        private static String ConvertIntegerNumber(String Number)
        {
            string word = string.Empty;
            bool isDone = false;
                double amount = Convert.ToDouble(Number);
            if (amount > 0)
            {
                int numDigits = Number.Length;
                int pos = 0;
                String place = "";
                switch (numDigits)
                {
                    case 1:
                        word = ConvertSingleDigitNumber(Number);
                        isDone = true;
                        break;
                    case 2:
                        word = ConvertDoubleDigitNumber(Number);
                        isDone = true;
                        break;
                    case 3:
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4:
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10:
                    case 11:
                    case 12:
                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    case 13:
                    case 14:
                    case 15:
                        pos = (numDigits % 13) + 1;
                        place = " Trillion ";
                        break;
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        word = ConvertIntegerNumber(Number.Substring(0, pos)) + place + ConvertIntegerNumber(Number.Substring(pos));

                    }
                    else
                    {
                        word = ConvertIntegerNumber(Number.Substring(0, pos)) + ConvertIntegerNumber(Number.Substring(pos));
                    }
                }
                if (word.Trim().Equals(place.Trim())) word = "";
            }
            return word.Trim();
        }

        /// <summary>
        /// Method to conver digit after decimal in words
        /// </summary>
        /// <param name="number">Number after Decimal</param>
        /// <returns>Word format of Digits after Decimal</returns>
        private static String ConvertNumberAfterDecimal(String number)
        {
            String convertedString = "", decimaldigit = "", decimalDigitInWord = "";
            for (int i = 0; i < number.Length; i++)
            {
                decimaldigit = number[i].ToString();
                if (decimaldigit.Equals("0"))
                {
                    decimalDigitInWord = "Zero";
                }
                else
                {
                    decimalDigitInWord = ConvertSingleDigitNumber(decimaldigit);
                }
                convertedString += decimalDigitInWord + " ";
            }
            return convertedString.Trim();
        }

        /// <summary>
        /// Method to Convert Double Digit Number into Words
        /// </summary>
        /// <param name="number">Double Digit Number</param>
        /// <returns>Word format of Double Digit Number</returns>
        private static String ConvertDoubleDigitNumber(String number)
        {
            int Number = Convert.ToInt32(number);
            switch (Number)
            {
                case 10:
                    return "Ten";
                case 11:
                    return "Eleven";
                case 12:
                    return "Twelve";
                case 13:
                    return "Thirteen";
                case 14:
                    return "Fourteen";
                case 15:
                    return "Fifteen";
                case 16:
                    return "Sixteen";
                case 17:
                    return "Seventeen";
                case 18:
                    return "Eighteen";
                case 19:
                    return "Nineteen";
                case 20:
                    return "Twenty";
                case 30:
                    return "Thirty";
                case 40:
                    return "Fourty";
                case 50:
                    return "Fifty";
                case 60:
                    return "Sixty";
                case 70:
                    return "Seventy";
                case 80:
                    return "Eighty";
                case 90:
                    return "Ninety";
                default:
                    if (Number > 0)
                    {
                        return ConvertDoubleDigitNumber(number.Substring(0, 1) + "0") + " " + ConvertSingleDigitNumber(number.Substring(1));
                    }
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// Method to Convert Single Digit into Words
        /// </summary>
        /// <param name="Number">Single Digit Number</param>
        /// <returns>Word format of Single Digit Number</returns>
        private static String ConvertSingleDigitNumber(String Number)
        {
            int singleNumber = Convert.ToInt32(Number);
            string result = string.Empty;
            switch(singleNumber)
            {
                case 1:
                    result = "One";
                    break;
                case 2:
                    result = "Two";
                    break;
                case 3:
                    result = "Three";
                    break;
                case 4:
                    result = "Four";
                    break;
                case 5:
                    result = "Five";
                    break;
                case 6:
                    result = "Six";
                    break;
                case 7:
                    result = "Seven";
                    break;
                case 8:
                    result = "Eight";
                    break;
                case 9:
                    result = "Nine";
                    break;
                default:
                    break;
            }
            return result;
        }
        #endregion
    }
}
