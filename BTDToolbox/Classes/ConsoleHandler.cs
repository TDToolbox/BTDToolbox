using System;
using System.Windows.Forms;

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
        public static void force_appendLog(String log)
        {
            if (validateConsole())
                console.force_appendLog(log);            
        }

        public static void announcement()
        {
            if (validateConsole())
                console.GetAnnouncement();
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
