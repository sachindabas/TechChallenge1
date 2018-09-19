using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInWords.Core
{
    public static class Constants
    {
        #region Logger
        public const string LogAppenderName = "CustomLog";
        #endregion

        #region Cache Manager
        public const string CacheConnectionString = "CacheConnectionString";
        public const string CacheKey = "{0}";
        public const int DefaultCacheDuration = 24;
        #endregion

        #region Error Messages
        public const string NumberRangeValidationError = "Number should be between -999999999999999.99 to 999999999999999.99";
        public const string NumberRequiredValidationError = "Number is required";
        public const string NameRequiredValidationError = "Name is required";
        public const string GenericErrorMessage = "Oops! Something went wrong. Please try again";
        #endregion

        #region Number 
        public const decimal MaxLimit = (decimal)999999999999999.99;
        public const decimal MinLimit = (decimal)-999999999999999.99;
        #endregion
    }
}
