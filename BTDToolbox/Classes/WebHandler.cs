using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BTDToolbox.Classes
{
    class WebHandler
    {
        WebHandler reader;
        
        bool exitLoop = false;
        public string startURL { get; set; }
        public string readURL { get; set; }
        public bool urlAquired { get; set; }

        

        public bool checkWebsite(string URL)
        {
            try
            {
                WebClient wc = new WebClient();
                string HTMLSource = wc.DownloadString(URL);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void TryReadURL_StartThread()    
        {
            Thread thread = new Thread(() => urlAquired =TryReadURL_OnThread(startURL));
            thread.Start();
        }
        private bool TryReadURL_OnThread(string url)    //Do not call this one
        {
            bool success = false;
            WebClient client = new WebClient();

            try
            {
                readURL = client.DownloadString(url);
                success = true;
            }
            catch
            {
                if (success == false)
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        Thread.Sleep(100);
                        try
                        {
                            if (!exitLoop)
                            {
                                readURL = client.DownloadString(url);
                                if (readURL != null && readURL != "")
                                {
                                    success = true;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch { }
                    }
                }
            }
            return success;
        }
        /// <summary>
        /// Complete method. Waits on URL then reads and returns text from it
        /// </summary>
        /// <param name="url">url to read text from</param>
        /// <returns>text read off of website</returns>
        public string WaitOn_URL(string url)   //call this one to read the text from the git url
        {
            WebHandler get = new WebHandler();
            get.startURL = url;
            get.TryReadURL_StartThread();

            for (int i = 0; i <= 100; i++)
            {
                GeneralMethods.checkedForExit();
                Thread.Sleep(100);
                if (get.urlAquired)
                {
                    break;
                }
            }
            if (get.readURL == null)
                get.readURL = "";
            return get.readURL;
        }      
        public string processGit_Text(string url, string deleteText, int lineNumber)    //call this one read git text and return the url we want. Delete text is the starting word, for example "toolbox2019: "
        {
            if (url != null && url != "")
            {
                string[] split = url.Split('\n');
                return split[lineNumber].Replace(deleteText, "");
            }
            else
            {
                return null;
            }
        }
        public string Get_GitVersion(string url)    // will read processed git url and return a git version number
        {
            if(url != null)
            {

                string[] version = url.Split('/');
                return (version[version.Length - 2]).Replace(".", "");
            }
            else
            {
                return "";
            }
        }
        public  bool CheckForUpdate(string url, string deleteText, int lineNumber, string currentVersion) //Use this to check for updates
        {
            reader = new WebHandler();
            string processedUrl = reader.processGit_Text(reader.WaitOn_URL(url), deleteText, lineNumber);
            string gitVersion = reader.Get_GitVersion(processedUrl);
            string toolboxVersion = "";

            int number = 0; ;
            foreach (char c in currentVersion)
            {
                if (Int32.TryParse(c.ToString(), out number))
                    toolboxVersion = toolboxVersion + c;
            }
            if(gitVersion != null && gitVersion != "")
            {
                if (Int32.Parse(toolboxVersion) < Int32.Parse(gitVersion))
                    return true;
                else
                    return false;
            }
            else
            {
                ConsoleHandler.append("Unable to determine latest version of BTD Toolbox");
                return false;
            }
        }
    }
}
