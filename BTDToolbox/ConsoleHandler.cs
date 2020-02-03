using System;

namespace BTDToolbox
{
    class ConsoleHandler
    {
        public static NewConsole console;

        public static void appendLog(String log)
        {
            if (validateConsole())
                console.appendLog(log);
            
        }
        public static bool validateConsole()
        {
            if (NewConsole.getInstance() == null)
            {
                return false;
            }
            else
                return true;
        }
    }
}
