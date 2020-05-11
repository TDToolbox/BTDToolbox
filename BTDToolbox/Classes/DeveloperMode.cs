using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes
{
    class DeveloperMode
    {
        private static DeveloperMode mode;
        public static void ControlDeveloperMode()
        {
            if (mode == null)
                mode = new DeveloperMode();

            if (mode.UseDeveloperMode())
                mode.EnableDeveloperMode();
            else
                mode.DisableDeveloperMode();
        }
        private bool UseDeveloperMode()
        {
            if (Serializer.cfg.UseDeveloperMode)
                return true;
            else
                return false;
        }
        private void EnableDeveloperMode()
        {
            Main.getInstance().debugToolStripMenuItem.Visible = true;
        }
        private void DisableDeveloperMode()
        {
            Main.getInstance().debugToolStripMenuItem.Visible = false;
        }
    }
}
