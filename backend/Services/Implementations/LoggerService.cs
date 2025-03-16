using log4net;
using NexCart.Services.Interfaces;

namespace NexCart.Services.Implementations
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }
    }
}
