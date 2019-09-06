using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox
{
    class JsonProps
    {
        public static List<JsonEditor> jsonformlist = new List<JsonEditor>();
        public static void increment(JsonEditor form)
        {
            jsonformlist.Add(form);
            //ConsoleHandler.appendLog("Json windows opened: " + jsonformlist.Count);
        }
        public static void decrement(JsonEditor form)
        {
            jsonformlist.Remove(form);
            //ConsoleHandler.appendLog("Json windows opened: " + jsonformlist.Count);
        }
        public static List<JsonEditor> get()
        {
            //ConsoleHandler.appendLog("Json windows opened: " + jsonformlist.Count);
            return jsonformlist;
        }
        public static JsonEditor getForm(int num)
        {
            try
            {
                return jsonformlist[num];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
