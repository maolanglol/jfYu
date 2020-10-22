using NLog;

namespace jfYu.Core.Common.NLog
{
    public static class NlLogExtensions
    {

        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            logger.Log(ei);
        }
        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message, string content)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            ei.Properties["content"] = content;
            logger.Log(ei);
        }
        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message, string content, string url)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            ei.Properties["content"] = content;
            ei.Properties["url"] = url;
            logger.Log(ei);
        }
        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message, string content, string url,string ip)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            ei.Properties["content"] = content;
            ei.Properties["url"] = url;
            ei.Properties["ip"] = ip;
            logger.Log(ei);
        }
        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message, string content, string url, string userid, string username)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            ei.Properties["content"] = content;
            ei.Properties["url"] = url;
            ei.Properties["userid"] = userid;
            ei.Properties["username"] = username;
            logger.Log(ei);
        }
        public static void ALog(this ILogger logger, LogLevel logLevel, string logType, string message, string content, string url, string ip,string userid, string username)
        {

            LogEventInfo ei = new LogEventInfo(logLevel, logType, message);
            ei.Properties["content"] = content;
            ei.Properties["url"] = url;
            ei.Properties["ip"] = ip;
            ei.Properties["userid"] = userid;
            ei.Properties["username"] = username;
            logger.Log(ei);
        }
    }
}
