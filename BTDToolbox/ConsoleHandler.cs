using System;

namespace BTDToolbox
{
    class ConsoleHandler
    {
        public static NewConsole console;

        public static void appendLog(String log)
        {
            console.appendLog(log);
        }
    }
}
