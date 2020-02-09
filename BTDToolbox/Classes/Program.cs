using System;
using System.Windows.Forms;

namespace BTDToolbox
{
    static class Program
    {
        public static bool enableSplash;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Serializer.Deserialize_Config() == null)
            {
                Serializer.Deserialize_Config();
            }
            else
            {
                if (Serializer.Deserialize_Config().enableSplash == true)
                {
                    enableSplash = true;
                    Application.Run(new SplashScreen());
                }

                else
                {
                    enableSplash = false;
                    Application.Run(new Main());
                }
            }
            
        }
    }
}
