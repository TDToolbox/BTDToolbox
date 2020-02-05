using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes
{
    class ModLoader_Handling
    {
        public bool ValidateManager()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            if(dir.Exists)
            {
                if (File.Exists("\\ModLoader\\BTDModLoader.exe"))
                    return true;
            }
            return false;
        }
       /* public bool DownloadManager()
        {
            ReadURL getURL = new ReadURL();
            string url = getURL.WaitFor_URL(https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/Version)
        }*/

        public void DeleteManager()
        {

        }
        public bool CheckUpdates()
        {

            return false;
        }
        public void Launch()
        {

        }

    }
}
