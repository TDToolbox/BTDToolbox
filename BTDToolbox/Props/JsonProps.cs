using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox
{
    class JsonProps
    {
        public static List<JsonEditor> jsonformList = new List<JsonEditor>();
        public static void increment(JsonEditor form)
        {
            jsonformList.Add(form);
            //ConsoleHandler.append("Jet windows opened: " + jsonformList.Count);
        }
        public static void decrement(JsonEditor form)
        {
            jsonformList.Remove(form);
            //ConsoleHandler.append("Jet windows opened: " + jsonformList.Count);
        }
        public static List<JsonEditor> get()
        {
            //ConsoleHandler.append("Jet windows opened: " + jsonformList.Count);
            return jsonformList;
        }
        public static JsonEditor getForm(int num)
        {
            try
            {
                return jsonformList[num];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
