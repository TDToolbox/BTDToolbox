using System;
using System.IO;

namespace BTDToolbox
{
    class Settings
    {
        public static string readGamePath()
        {
            string livePath = Environment.CurrentDirectory;
            if (!File.Exists(livePath + "\\launchSettings.txt"))
            {
                return "";
            }
            StreamReader sw = new StreamReader(livePath + "\\launchSettings.txt");
            string path = sw.ReadLine();
            sw.Close();
            return path;
        }
        public static void setGamePath(string path)
        {
            string livePath = Environment.CurrentDirectory;
            StreamWriter stream = new StreamWriter(livePath + "\\launchSettings.txt", false);
            stream.Write(path);
            stream.Close();
        }
    }
}
