using System;

namespace NumberInWords.Core
{
    public interface ILogger
    {
        /// <summary>
        /// write error in the log
        /// </summary>
        /// <param name="message">error message</param>
        void WriteError(string message);

        /// <summary>
        /// write error in log
        /// </summary>
        /// <param name="message">error message</param>
        /// <param name="ex">exception</param>
        void WriteError(string message, Exception ex);

        /// <summary>
        /// write information in the log
        /// </summary>
        /// <param name="message">message</param>
        void WriteInfo(string message);

        /// <summary>
        /// write debug messages in log
        /// </summary>
        /// <param name="message">message</param>
        void WriteDebug(string message);

        /// <summary>
        /// write debug messages in log
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="ex">exception</param>
        void WriteDebug(string message, Exception ex);

        /// <summary>
        /// write warning in the log
        /// </summary>
        /// <param name="message">message</param>
        void WriteWarning(string message);

        /// <summary>
        /// write exception in the log
        /// </summary>
        /// <param name="ex">exception</param>
        void WriteException(Exception ex);
    }
}
