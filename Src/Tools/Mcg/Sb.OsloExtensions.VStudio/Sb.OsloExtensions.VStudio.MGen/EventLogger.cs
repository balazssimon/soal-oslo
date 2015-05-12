using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace OsloExtensions.VisualStudio
{
    public class EventLogger
    {
        public const bool EnableLogging = true;

        private static string logger = "VisualStudioOsloExtensions";
        private static Dictionary<string, EventLogger> loggers;
        private static object mutex;
        private string name;

        static EventLogger()
        {
            EventLogger.loggers = new Dictionary<string, EventLogger>();
            EventLogger.mutex = new object();
        }

        private EventLogger(string name)
        {
            this.name = EventLogger.logger + "." + name;
        }

        public static EventLogger CreateLogger(string name)
        {
            EventLogger result = null;
            if (EventLogger.loggers.TryGetValue(name, out result))
            {
                return result;
            }
            lock (EventLogger.mutex)
            {
                if (EventLogger.loggers.TryGetValue(name, out result))
                {
                    return result;
                }
                result = new EventLogger(name);
                EventLogger.loggers.Add(name, result);
                return result;
            }
        }

        public void Log(EventLogEntryType level, string message)
        {
            if (EventLogger.EnableLogging)
            {
                Trace.Write(message);
            }
        }

    }
}
