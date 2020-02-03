using System;

namespace BTDToolbox
{
    class ConsoleHandler
    {
        public static Console console;

        public static void appendLog(String log)
        {
            if (validateConsole())
                console.appendLog(log);
            
        }
        public static bool validateConsole()
        {
            if (Console.getInstance() == null)
            {
                return false;
            }
            else
                return true;
        }
    }
}
