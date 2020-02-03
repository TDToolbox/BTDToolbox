using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox
{
    class JetProps
    {
        private static List<JetForm> jetformlist = new List<JetForm>();
        public static void increment(JetForm form)
        {
            jetformlist.Add(form);
            //ConsoleHandler.appendLog("Jet windows opened: " + jetformlist.Count);
        }
        public static void decrement(JetForm form)
        {
            jetformlist.Remove(form);
            //ConsoleHandler.appendLog("Jet windows opened: " + jetformlist.Count);
        }
        public static List<JetForm> get()
        {
            //ConsoleHandler.appendLog("Jet windows opened: " + jetformlist.Count);
            return jetformlist;
        }
        public static JetForm getForm(int num)
        {
            try
            {
                return jetformlist[num];
            } catch (Exception)
            {
                return null;
            }
        }
    }
}
