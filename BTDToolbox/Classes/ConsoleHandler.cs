using System;
using System.Windows.Forms;

namespace BTDToolbox
{
    class ConsoleHandler
    {
        public static Console console;

        public static void append(String log, bool canRepeat, bool force, bool notice)
        {
            if (!validateConsole())
                return;

            console.append(log, canRepeat, force, notice);
        }

        public static void append(String log) => append(log, false, false, false);
        public static void append_CanRepeat(String log) => append(log, true, false, false);
        public static void append_Force(String log) => append(log, false, true, false);
        public static void append_Force_CanRepeat(String log) => append(log, true, true, false);
        public static void append_Notice(String log) => append(log, false, false, true);
        public static void force_append_Notice(String log) => append(log, false, true, true);
        public static void force_append_Notice_CanRepeat(String log) => append(log, true, true, true);
        
       
        public static void announcement()
        {
            if (validateConsole())
                console.GetAnnouncement();
        }
        public static void ClearConsole()
        {
            if (validateConsole())
                console.Close();

            console = new Console();
            console.MdiParent = Main.getInstance();
            console.Show();
            console.BringToFront();
        }

        public static bool validateConsole()
        {
            if (Console.getInstance() == null)
                return false;
            else
                return true;
        }
    }
}
