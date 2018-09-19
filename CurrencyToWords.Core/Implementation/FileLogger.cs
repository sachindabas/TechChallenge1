using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberInWords.Core
{
    public class FileLogger : ILogger
    {
        private readonly ILog Logger;
        public FileLogger()
        {
            XmlConfigurator.Configure();
            Logger = LogManager.GetLogger(typeof(FileLogger));
        }

        public FileLogger(ILog logger)
        {
            if (logger != null)
            {
                this.Logger = logger;
            }
            else
            {
                Logger = LogManager.GetLogger(Constants.LogAppenderName);
            }
        }

        public void WriteError(string message)
        {
            Logger.Error(message);
        }

        public void WriteError(string message, Exception ex)
        {
            message = (message == "") ? (ex != null ? ex.Message : string.Empty) : message;
            Logger.Error(message, ex);
        }

        public void WriteInfo(string message)
        {
            Logger.Info(message);
        }

        public void WriteDebug(string message)
        {
            Logger.Debug(message);
        }

        public void WriteDebug(string message, Exception ex)
        {
            message = (message == "") ? (ex != null ? ex.Message : string.Empty) : message;
            Logger.Debug(message, ex);
        }

        public void WriteWarning(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteException(Exception ex)
        {
            Logger.Error(string.Empty, ex);
        }
    }
}
