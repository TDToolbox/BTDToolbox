using System;

namespace BTDToolbox
{
    class ConsoleHandler
    {
        public static Console console;

        public static void appendLog(String log)
        {
            console.appendLog(log);
        }
    }
}
