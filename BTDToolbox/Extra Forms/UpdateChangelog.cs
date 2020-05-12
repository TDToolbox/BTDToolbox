using BTDToolbox.Classes;
using BTDToolbox.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox
{

    public delegate void MyEventHandler(object source, MyEventArgs e);

    public partial class UpdateChangelog : ThemedForm
    {
        public event MyEventHandler GotChangelog;
        
        public UpdateChangelog()
        {
            InitializeComponent();
            this.TitleBar_LeftCorner.SendToBack();
            Sizer.Hide();
        }
        public UpdateChangelog(string changelogText):this()
        {
            this.moveCenterScreen = true;
            InitUpgadeChagelog(changelogText);
            Show();
            this.Location = new Point(this.Location.X, this.Location.Y - 50);
        }

        public void InitUpgadeChagelog(string changelogText)
        {  
            string[] split = changelogText.Split('\n');

            int y = -10;
            foreach (string line in split)
            {
                Color color = Color.White;
                string text = line;
                int size = 20;
                int x = 30;
                int height = 0;
                Label lbl = new Label();
                lbl.Location = new Point(x, y);
                if (line.StartsWith("#1"))
                {
                    text = line.Substring(2);
                    size = 40;
                    height = 10;
                    x = 0;
                    lbl.Location = new Point(x, y + 10);
                }
                if (line.StartsWith("#L"))
                {
                    text = line.Substring(2);
                    color = Color.FromArgb(0, 100, 255);
                    lbl.MouseClick += (sender, eventArgs) =>
                    {
                        Process.Start(lbl.Text);
                    };
                }
                lbl.Font = new Font(FontFamily.GenericSansSerif, size / 2);
                lbl.Text = text;
                lbl.ForeColor = color;
                lbl.BringToFront();
                lbl.Width = contentPanel.Width;
                lbl.Height += height;
                base.contentPanel.Controls.Add(lbl);
                lbl.Show();
                y += size;
            }
            //this.Size = new Size(this.Size.Width - 300, y + 25);
            this.Size = new Size(this.Size.Width-20, y + 45);

            Serializer.cfg.recentUpdate = false;
            Serializer.SaveSettings();
        }

        public void StartUpdateChangelog()
        {
            new Thread(() =>
            {
                WebHandler web = new WebHandler();
                string url = "https://raw.githubusercontent.com/TDToolbox/BTDToolbox-2019_LiveFIles/master/toolbox%20update%20changelog";
                string answer = web.WaitOn_URL(url);

                if (!Guard.IsStringValid(answer))
                {
                    ConsoleHandler.append("Failed to read update Changelog");
                    Serializer.cfg.recentUpdate = true;
                    Serializer.SaveSettings();
                    return;
                }

                if(GotChangelog !=null)
                {
                    GotChangelog(this, new MyEventArgs(answer));
                }
            }).Start();
        }
    }

    public class MyEventArgs : EventArgs
    {
        private string EventInfo;
        public MyEventArgs(string Text)
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }
}
