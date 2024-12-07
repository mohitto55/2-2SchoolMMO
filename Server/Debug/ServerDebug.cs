using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Debug
{
    public enum LogType
    {
        Log,
        Warning,
        Error
    }
    public static class ServerDebug
    {
        public static Action<string> LogEvent;
        public static void Log(LogType type, string message) {
            string typeStr = "[" + type.ToString() + "]";
            message = message.Insert(0, typeStr);
            if (LogEvent != null) LogEvent(message);
        }

        public static void Log(LogType type, string head, string[] messages)
        {
            if (messages == null && messages.Length == 0)
                return;

            string message = head;
            for(int i = 0; i < messages.Length; i++)
            {
                message += messages[i];
                if (i != messages.Length - 1)
                {
                    message += ", ";
                }
            }

            Log(type, message);
        }

        public static void Log(LogType type, string[] messages)
        {
            Log(type, "", messages);
        }
    }
}
