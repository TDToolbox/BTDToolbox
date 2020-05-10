using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BTDToolbox.GeneralMethods;
using static BTDToolbox.Classes.WebHandler;

namespace BTDToolbox.Classes
{
    class Announcements
    {
        public static void GetAnnouncement()
        {
            WebHandler web = new WebHandler();
            string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20announcements";
            string answer = web.WaitOn_URL(url);

            
            ConsoleHandler.append(answer);
        }
    }
}
